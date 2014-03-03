#region "Header information"
/*
===============================================================================
Company Name    :   SteerRent
Project Name    :   SteerRent
File            :   SSBTraceLister.cs
Module/Component:   Common
Created By      :   Sivaprakasam S
Created Date    :   22/09/2013
Description     :   Logging Constants for logging
Purpose         :   Logging
Reviewed By     :
Reviewed Date   :

Revision History
-------------------------------------------------------------------------------
Sl.No | Modified By | Modified Date |              Remarks
-------------------------------------------------------------------------------
1.    |             |               |  
===============================================================================
*/

#endregion

namespace SteerRent.Logging
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

    /// <summary>
    /// The SSB TraceListener is used to log the data to the SQL Service Broker
    /// </summary>
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class SSBTraceListener : CustomTraceListener
    {
        #region Private Members
        /// <summary>
        /// THe App settings reader is used to read the data from the config file
        /// </summary>
        private AppSettingsReader _appReader = new AppSettingsReader();
        
        /// <summary>
        /// Connection string
        /// </summary>
        private SqlConnection sqlConntoSSB;
        #endregion
       
        #region Overridden Methods
        /// <summary>
        /// Trace Data is used to trace the data sent from the user
        /// </summary>
        /// <param name="eventCache">Event Cache</param>
        /// <param name="source">Source of origin</param>
        /// <param name="eventType">The type of the event</param>
        /// <param name="id">The unique id</param>
        /// <param name="data">The data to be logged</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (data is LogEntry && this.Formatter != null)
            {
                this.WriteLine(this.Formatter.Format(data as LogEntry));
            }
            else
            {
                this.WriteLine(data.ToString());
            }
        }

        /// <summary>
        /// This method is used to write data
        /// </summary>
        /// <param name="message">Message to write</param>
        public override void Write(string message)
        {
            this.WriteToSSB(message);
        }

        /// <summary>
        /// This method is used to write a line of data
        /// </summary>
        /// <param name="message">Message to be write</param>
        public override void WriteLine(string message)
        {
            this.WriteToSSB(message);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Method that sends the message to the queue
        /// </summary>
        /// <param name="messageToQueue">Message to be sent to the queue</param>
        private void SendToQueue(string messageToQueue)
        {
            try
            {
                string _queueName = null;

                SqlTransaction sqlTranstoSSB = this.sqlConntoSSB.BeginTransaction();
                string logInitiator = this._appReader.GetValue("InitiatorService", typeof(string)) as string;
                string logReceiver = this._appReader.GetValue("ReceiverService", typeof(string)) as string;
                string contractName = this._appReader.GetValue("LogContract", typeof(string)) as string;
                string messageType = this._appReader.GetValue("LogMessage", typeof(string)) as string;
                string messageToLog = messageToQueue;

                // Identifying the service
                SqlCommand identifyQueue = this.sqlConntoSSB.CreateCommand();
                identifyQueue.CommandText = "SELECT q.name "
                    + "FROM sys.service_queues q JOIN sys.services as s "
                    + "ON s.service_queue_id = q.object_id "
                    + "WHERE s.name = @sname";
                identifyQueue.Transaction = sqlTranstoSSB;

                SqlParameter paramIdentifyQueue;
                paramIdentifyQueue = identifyQueue.Parameters.Add("@sname", SqlDbType.NChar, 255);
                paramIdentifyQueue.Value = logInitiator;
                _queueName = (string)identifyQueue.ExecuteScalar();

                if (_queueName == null)
                {
                    throw new ArgumentException(
                        "Could not find any service with the name in this database.");
                }

                // Begining the dialog
                SqlParameter paramBeginDialog;
                SqlCommand beginDialog = this.sqlConntoSSB.CreateCommand();

                StringBuilder queryBeginDialog = new StringBuilder();
                queryBeginDialog.Append("BEGIN DIALOG @ch FROM SERVICE @fs TO SERVICE @ts ON CONTRACT @cn WITH ENCRYPTION = ");
                queryBeginDialog.Append("OFF ");
                queryBeginDialog.Append(", LIFETIME = ");
                queryBeginDialog.Append((long)TimeSpan.FromMinutes(1).TotalSeconds);
                queryBeginDialog.Append(' ');
                paramBeginDialog = beginDialog.Parameters.Add("@ch", SqlDbType.UniqueIdentifier);
                paramBeginDialog.Direction = ParameterDirection.Output;
                paramBeginDialog = beginDialog.Parameters.Add("@fs", SqlDbType.NVarChar, 255);
                paramBeginDialog.Value = logInitiator;
                paramBeginDialog = beginDialog.Parameters.Add("@ts", SqlDbType.NVarChar, 255);
                paramBeginDialog.Value = logReceiver;
                paramBeginDialog = beginDialog.Parameters.Add("@cn", SqlDbType.NVarChar, 128);
                paramBeginDialog.Value = contractName;
                beginDialog.CommandText = queryBeginDialog.ToString();
                beginDialog.Transaction = sqlTranstoSSB;

                beginDialog.ExecuteNonQuery();

                paramBeginDialog = beginDialog.Parameters["@ch"] as SqlParameter;
                Guid handle = (Guid)paramBeginDialog.Value;

                // Sending the message
                SqlParameter paramSendMsg;
                SqlCommand cmdSendMsg = this.sqlConntoSSB.CreateCommand();
                cmdSendMsg.Transaction = sqlTranstoSSB;

                string querySendMsg = "SEND ON CONVERSATION @ch MESSAGE TYPE @mt ";
                paramSendMsg = cmdSendMsg.Parameters.Add("@ch", SqlDbType.UniqueIdentifier);
                paramSendMsg.Value = handle;
                paramSendMsg = cmdSendMsg.Parameters.Add("@mt", SqlDbType.NVarChar, 255);
                paramSendMsg.Value = messageType;
                querySendMsg += " (@msg)";
                paramSendMsg = cmdSendMsg.Parameters.Add("@msg", SqlDbType.VarBinary, -1);
                Stream messagetosend = new MemoryStream(Encoding.UTF8.GetBytes(messageToLog));
                paramSendMsg.Value = new SqlBytes(messagetosend);
                cmdSendMsg.CommandText = querySendMsg.ToString();
                cmdSendMsg.ExecuteNonQuery();
                sqlTranstoSSB.Commit();
            }
            catch (ArgumentException argEx)
            {
                throw argEx;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
        }

        #region Service Broker Writer
        /// <summary>
        /// Method that writes to the Service Broker
        /// </summary>
        /// <param name="message">Message to be written</param>
        private void WriteToSSB(string message)
        {
            //string connString = ConfigurationManager.ConnectionStrings["ConnectionStringtoSSB"].ConnectionString;
            //this.sqlConntoSSB = new SqlConnection(connString);
            //this.sqlConntoSSB.Open();
            //this.SendToQueue(message);
            //this.sqlConntoSSB.Close();
            LogMessage(message);
        }


        /// <summary>
        /// Loging information in text file in log folder. --- purushotham
        /// </summary>
        /// <param name="message"></param>
        private static void LogMessage(string message)
        {
            try
            {
                if (!System.IO.Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log"))
                    System.IO.Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log");

                using (StreamWriter sw = File.AppendText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log\\SteerRent_Log.txt"))
                {
                    sw.WriteLine(message);
                    sw.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        #endregion
    }
}

#region "Header information"
/*
===============================================================================
Company Name    :   SteerRent
Project Name    :   SteerRent
File            :   SRLogger.cs
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Practices.EnterpriseLibrary.Logging;

    /// <summary>
    /// The SRLogger class serves as a wrapper for the Enterprise Library Logging Application Block. 
    /// </summary>
    public static class SRLogger
    {
        #region Info
        /// <summary>
        /// Log a message with a Severity of Information
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Info(string message)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Information, LoggingConstants.PRIORITY_INFO, null, null);
            Logger.Write(entry);
        }
        /// <summary>
        /// Log a message with a Severity of Information with Time
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Info(string message, DateTime TimeStamp)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Information, LoggingConstants.PRIORITY_INFO, null, null, TimeStamp);
            Logger.Write(entry);
        }
        /// <summary>
        /// Log a message with a Severity of Information with the given category
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="category">The category describes the module from which the message is logged.  </param>
        public static void Info(string message, string category)
        {
            List<string> categories = new List<string>();
            categories.Add(category);
            LogEntry entry = CreateLogEntry(message, TraceEventType.Information, LoggingConstants.PRIORITY_INFO, categories, null);
            Logger.Write(entry);
        }
        #endregion

        #region Verbose
        /// <summary>
        /// Log a message with a Severity of Verbose
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Verbose(string message)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Verbose, LoggingConstants.PRIORITY_VERBOSE, null, null);
            Logger.Write(entry);
        }
        /// <summary>
        /// Log a message with a Severity of Verbose with Time
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Verbose(string message, DateTime TimeStamp)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Verbose, LoggingConstants.PRIORITY_VERBOSE, null, null, TimeStamp);
            Logger.Write(entry);
        }
        /// <summary>
        /// Log a message with a Severity of Verbose with the given category
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="category">The category.  Note that this will be in addition to any categories that are in effect
        /// due to a Tracer being active</param>
        public static void Verbose(string message, string category)
        {
            List<string> categories = new List<string>();
            categories.Add(category);
            LogEntry entry = CreateLogEntry(message, TraceEventType.Verbose, LoggingConstants.PRIORITY_VERBOSE, categories, null);
            Logger.Write(entry);
        }

        #endregion

        #region Error
        /// <summary>
        /// Log a message with a Severity of Error
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Error(string message)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Error, LoggingConstants.PRIORITY_CRITICAL_ERROR, null, null);
            Logger.Write(entry);
        }
        /// <summary>
        /// Log a message with a Severity of Error with Time
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Error(string message, DateTime TimeStamp)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Error, LoggingConstants.PRIORITY_CRITICAL_ERROR, null, null, TimeStamp);
            Logger.Write(entry);
        }
        /// <summary>
        /// Log a message with a Severity of Error with the given category
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="category">The category.  Note that this will be in addition to any categories that are in effect
        /// due to a Tracer being active</param>
        public static void Error(string message, string category)
        {
            List<string> categories = new List<string>();
            categories.Add(category);
            LogEntry entry = CreateLogEntry(message, TraceEventType.Error, LoggingConstants.PRIORITY_CRITICAL_ERROR, categories, null);
            Logger.Write(entry);
        }
        #endregion

        #region Critical
        /// <summary>
        /// Log a message with a Severity of Critical
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Critical(string message)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Critical, LoggingConstants.PRIORITY_CRITICAL_ERROR, null, null);
            Logger.Write(entry);
        }
        /// <summary>
        /// Log a message with a Severity of Critical with Time
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Critical(string message, DateTime TimeStamp)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Critical, LoggingConstants.PRIORITY_CRITICAL_ERROR, null, null, TimeStamp);
            Logger.Write(entry);
        }        
        /// <summary>
        /// Log a message with a Severity of Critical with the given category
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="category">The category.  Note that this will be in addition to any categories that are in effect
        /// due to a Tracer being active</param>
        public static void Critical(string message, string category)
        {
            List<string> categories = new List<string>();
            categories.Add(category);
            LogEntry entry = CreateLogEntry(message, TraceEventType.Critical, LoggingConstants.PRIORITY_CRITICAL_ERROR, categories, null);
            Logger.Write(entry);
        }

        #endregion
        #region Warn
        /// <summary>
        /// Log a message with a Severity of Warning
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Warn(string message)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Warning, LoggingConstants.PRIORITY_WARN, null, null);
            Logger.Write(entry);
        }
        /// <summary>
        /// Log a message with a Severity of Warning with Time
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Warn(string message, DateTime TimeStamp)
        {
            LogEntry entry = CreateLogEntry(message, TraceEventType.Warning, LoggingConstants.PRIORITY_WARN, null, null, TimeStamp);
            Logger.Write(entry);
        }
        /// <summary>
        /// Log a message with a Severity of Warning with the given category
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="category">The category.  Note that this will be in addition to any categories that are in effect
        /// due to a Tracer being active</param>
        public static void Warn(string message, string category)
        {
            List<string> categories = new List<string>();
            categories.Add(category);
            LogEntry entry = CreateLogEntry(message, TraceEventType.Warning, LoggingConstants.PRIORITY_WARN, categories, null);
            Logger.Write(entry);
        }

        #endregion

        #region Private methods

        #region Construct Log Entry
        /// <summary>
        /// A private method to build the log entry 
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="severity">Severity of the message</param>
        /// <param name="priority">Priority of the message</param>
        /// <param name="categories">Categories to be added</param>
        /// <param name="extendedProperties">The other extended properties</param>
        /// <returns>LogEntry object</returns>
        private static LogEntry CreateLogEntry(string message, TraceEventType severity, int priority, ICollection<string> categories, IDictionary<string, object> extendedProperties)
        {
            LogEntry entry = new LogEntry();
            entry.Categories.Add(LoggingConstants.DEFAULT_CATEGORY);
            entry.Severity = severity;
            entry.Message = entry.Severity.ToString()+" | "+message;
            entry.Priority = priority;
            entry.TimeStamp = DateTime.Now;

            if (categories != null)
            {
                foreach (string s in categories)
                {
                    entry.Categories.Add(s);
                }
            }

            if (extendedProperties != null)
            {
                foreach (string s in extendedProperties.Keys)
                {
                    entry.ExtendedProperties.Add(s, extendedProperties[s]);
                }
            }

            // If a Tracer is in effect then grab the id so that it will be available for people to stick in the log output if they want
            if (Trace.CorrelationManager.ActivityId != Guid.Empty)
            {
                entry.ExtendedProperties.Add("Trace.CorrelationManager.ActivityId", Trace.CorrelationManager.ActivityId);
            }

            return entry;
        }

        /// <summary>
        /// A private method to build the log entry 
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="severity">Severity of the message</param>
        /// <param name="priority">Priority of the message</param>
        /// <param name="categories">Categories to be added</param>
        /// <param name="extendedProperties">The other extended properties</param>
        /// <returns>LogEntry object</returns>
        private static LogEntry CreateLogEntry(string message, TraceEventType severity, int priority, ICollection<string> categories, IDictionary<string, object> extendedProperties, DateTime TimeStamp)
        {
            LogEntry entry = new LogEntry();
            entry.Categories.Add(LoggingConstants.DEFAULT_CATEGORY);
            entry.Severity = severity;
            entry.Message = message;
            entry.Priority = priority;
            entry.TimeStamp = TimeStamp;            
            
            if (categories != null)
            {
                foreach (string s in categories)
                {
                    entry.Categories.Add(s);
                }
            }

            if (extendedProperties != null)
            {
                foreach (string s in extendedProperties.Keys)
                {
                    entry.ExtendedProperties.Add(s, extendedProperties[s]);
                }
            }

            // If a Tracer is in effect then grab the id so that it will be available for people to stick in the log output if they want
            if (Trace.CorrelationManager.ActivityId != Guid.Empty)
            {
                entry.ExtendedProperties.Add("Trace.CorrelationManager.ActivityId", Trace.CorrelationManager.ActivityId);
            }

            return entry;
        }
        #endregion
        #endregion       
    }
}

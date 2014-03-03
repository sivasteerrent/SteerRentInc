#region "Header information"
/*
===============================================================================
Company Name    :   SteerRent
Project Name    :   SteerRent
File            :   LoggingConstants.cs
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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This class provides a place to store certain constants used for all log messages so that if we ever want to change
    /// them, it can be done in a single place.
    /// </summary>
    public class LoggingConstants
    {
        /// <summary>
        /// Severity types error or critical
        /// </summary>
        public const int PRIORITY_CRITICAL_ERROR = 1;  

        /// <summary>
        /// Severity type Warning
        /// </summary>
        public const int PRIORITY_WARN = 2; 

        /// <summary>
        /// Severity type Information
        /// </summary>
        public const int PRIORITY_INFO = 3;            

        /// <summary>
        /// severity type Verbose
        /// </summary>
        public const int PRIORITY_VERBOSE = 4;         

        /// <summary>
        /// The default category to be used by all log messages
        /// </summary>
        public const string DEFAULT_CATEGORY = "[SteerRent]";  
    }
}

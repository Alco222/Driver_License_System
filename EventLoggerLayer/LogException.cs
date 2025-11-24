using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    /// summary:
    ///  This Class is a Logging Exception
    public class LogException
    {
        /// summary:
        ///  This Field is a  Source Name for Event Logger.
        public static string SourceName = "DVLD_System";

        /// summary:
        /// This method creates the event logger in the event viewer.
        /// <param name="EventMessage"> Message For Event Log</param>
        /// <param name="eventLogEntryType">Icon or type of the event log</param>
        public static void logException(string EventMessage,EventLogEntryType eventLogEntryType)
       {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                    EventLog.CreateEventSource(SourceName, "Application");

                EventLog.WriteEntry(SourceName,EventMessage,eventLogEntryType);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(SourceName, $"Exception in LogException Method: {ex.Message}.", EventLogEntryType.Error);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Patterns.Adapter;

namespace Patterns.ChainOfResponsability
{
    public class DatabaseLogHandler: LogHandler
    {
        public override void HandleLog(string message, string type)
        {
            if (type == "database")
            {
                DataBaseLogger dataBaseLogger = DataBaseLogger.getInstance;
                Logger dataBaseLoggerAdapter = new DataBaseLoggerAdapter(dataBaseLogger);
                dataBaseLoggerAdapter.logMessage(DateTime.Now.ToString(message));
            }
            else if (successor != null)
            {
                successor.HandleLog(message, type);
            }

        }
    }
}

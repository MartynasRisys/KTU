using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Patterns.Adapter;

namespace Patterns.ChainOfResponsability
{
    public class FileLogHandler: LogHandler
    {
        public override void HandleLog(string message, string type)
        {
            if (type == "file")
            {
                FileLogger fileLogger = FileLogger.getInstance;
                Logger fileLoggerAdapter = new FileLoggerAdapter(fileLogger);
                fileLoggerAdapter.logMessage(message);
            }
            else if (successor != null)
            {
                successor.HandleLog(message, type);
            }
        }
    }
}

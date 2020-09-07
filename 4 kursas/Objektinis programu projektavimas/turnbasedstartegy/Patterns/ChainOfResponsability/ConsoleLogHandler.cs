using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Patterns.Adapter;

namespace Patterns.ChainOfResponsability
{
    public class ConsoleLogHandler: LogHandler
    {
        public override void HandleLog(string message, string type)
        {
            if (type == "console")
            {
                ConsoleLogger consoleLogger = ConsoleLogger.getInstance;
                consoleLogger.logMessage(message);
            }
            else if (successor != null)
            {
                successor.HandleLog(message, type);
            }
        }
    }
}

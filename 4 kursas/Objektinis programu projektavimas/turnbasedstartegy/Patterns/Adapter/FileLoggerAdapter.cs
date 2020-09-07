using System;
using System.Collections.Generic;
using System.Text;

namespace Patterns.Adapter
{
    public class FileLoggerAdapter : Logger
    {
        public FileLogger fileLogger;

        public FileLoggerAdapter(FileLogger newFileLogger)
        {
            fileLogger = newFileLogger;
        }

        public void logMessage(String message)
        {
            fileLogger.logMessageToFile(message);
        }
    }
}

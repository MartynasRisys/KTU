using System;
using System.Collections.Generic;
using System.Text;

namespace Patterns.Adapter
{
    public class DataBaseLoggerAdapter : Logger
    {
        public DataBaseLogger dataBaseLogger;

        public DataBaseLoggerAdapter(DataBaseLogger newDataBaseLogger)
        {
            dataBaseLogger = newDataBaseLogger;
        }

        public void logMessage(String message)
        {
            dataBaseLogger.logMessageToDataBase(message);
        }
    }
}

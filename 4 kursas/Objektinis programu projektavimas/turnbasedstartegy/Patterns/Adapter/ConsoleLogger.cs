using System;
using System.Collections.Generic;
using System.Text;

namespace Patterns.Adapter
{
    public class ConsoleLogger : Logger
    {
        private static ConsoleLogger instance = null;
        private static readonly object padlock = new object();
        private ConsoleLogger()
        {
        }
        public static ConsoleLogger getInstance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ConsoleLogger();
                    }
                    return instance;
                }
            }
        }
        public void logMessage(String message)
        {
            Console.WriteLine("{0}: {1}", DateTime.Now, message);
        }
    }
}

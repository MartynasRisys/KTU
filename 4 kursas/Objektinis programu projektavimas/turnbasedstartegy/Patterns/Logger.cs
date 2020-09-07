using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns
{
    public sealed class Logger
    {
        private static Logger instance = null;
        private static readonly object padlock = new object();
        private Logger()
        {
        }
        public static Logger getInstance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Logger();
                    }
                    return instance;
                }
            }
        }

        public void logMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}

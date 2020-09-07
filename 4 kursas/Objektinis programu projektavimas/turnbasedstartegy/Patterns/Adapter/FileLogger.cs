using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Patterns.Adapter
{
    public class FileLogger
    {
        private static FileLogger instance = null;
        private static readonly object padlock = new object();
        private FileLogger()
        {
        }
        public static FileLogger getInstance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FileLogger();
                    }
                    return instance;
                }
            }
        }

        public void logMessageToFile(String message)
        {
            using(StreamWriter writer = File.AppendText("logs.txt"))
            {
                writer.WriteLine("{0}: {1}", DateTime.Now, message);
            }
        }
    }
}

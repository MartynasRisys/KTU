using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Patterns.Adapter
{
    public class DataBaseLogger
    {
        private static DataBaseLogger instance = null;
        private static readonly object padlock = new object();
        private DataBaseLogger()
        {
        }
        public static DataBaseLogger getInstance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataBaseLogger();
                    }
                    return instance;
                }
            }
        }

        public async void logMessageToDataBase(String message)
        {
            HttpClient client = new HttpClient();

            long UserId = -1;
            String Username = "";
            String Message = message;
            JObject logObj = new JObject();
            logObj["userId"] = UserId;
            logObj["username"] = Username;
            logObj["message"] = Message;

            string url = "http://localhost:5000/api/logs";
            var response = await client.PostAsync(url, new StringContent(logObj.ToString(), Encoding.UTF8, "application/json"));
        }
    }
}

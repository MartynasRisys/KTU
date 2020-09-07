using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Log
    {
        public long Id { get; set; }
        public long UserId {get;set;}
        public String Username { get; set; }
        public String Message { get; set; }
        public DateTime Date { get; set; }

        public Log()
        {

        }

        public Log(long userId, String username, String message)
        {
            UserId = userId;
            Username = username;
            Message = message;
            Date = DateTime.Now;
        }
    }
}

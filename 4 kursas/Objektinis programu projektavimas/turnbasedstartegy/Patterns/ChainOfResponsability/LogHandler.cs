using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Patterns.ChainOfResponsability
{
    public abstract class LogHandler
    {
        protected LogHandler successor;

        public void SetSuccessor(LogHandler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleLog(string message, string type);
    }
}

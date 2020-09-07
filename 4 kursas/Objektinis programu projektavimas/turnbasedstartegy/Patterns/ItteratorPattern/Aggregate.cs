using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Patterns.ItteratorPattern
{
    public abstract class Aggregate

    {
        public abstract Iterator CreateIterator();
    }
}

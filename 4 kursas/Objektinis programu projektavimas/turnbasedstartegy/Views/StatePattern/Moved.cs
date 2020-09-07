using System;
using System.Collections.Generic;
using System.Text;
using Views;
using Models;

namespace Views.StatePattern
{
    public class Moved : State
    {
        public Moved()
        {
            AvailableStates.Clear();
            AvailableStates.Add(States.EndedTurn);
            AvailableStates.Add(States.UsedAction);

            Name = States.Moved;
        }

        public override State CreateDefaultState()
        {
            return new EndedTurn();
        }
    }
}

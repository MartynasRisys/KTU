using System;
using System.Collections.Generic;
using System.Text;
using Views;
using Models;

namespace Views.StatePattern
{
    public class Waiting : State
    {
        public Waiting()
        {
            AvailableStates.Clear();
            AvailableStates.Add(States.Moved);
            AvailableStates.Add(States.UsedAction);
            AvailableStates.Add(States.EndedTurn);

            Name = States.Waiting;
        }

        public override State CreateDefaultState()
        {
            return new Moved();
        }
    }
}

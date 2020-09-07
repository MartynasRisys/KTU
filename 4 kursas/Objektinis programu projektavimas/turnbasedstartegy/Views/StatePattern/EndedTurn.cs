using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Views.StatePattern
{
    public class EndedTurn : State
    {
        public EndedTurn()
        {
            AvailableStates.Clear();
            AvailableStates.Add(States.Waiting);

            Name = States.EndedTurn;
        }

        public override State CreateDefaultState()
        {
            return new Waiting();
        }
    }
}

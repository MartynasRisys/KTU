using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Views.StatePattern
{
    public class UsedAction : State
    {
        public UsedAction()
        {
            AvailableStates.Clear();
            AvailableStates.Add(States.EndedTurn);

            Name = States.UsedAction;
        }

        public override State CreateDefaultState()
        {
            return new EndedTurn();
        }
    }
}

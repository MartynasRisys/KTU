using Models;
using System.Collections.Generic;
using Views;

namespace Views.StatePattern
{
    public abstract class State
    {
        protected States Name;
        protected List<States> AvailableStates = new List<States>();

        public void TransitionTo(BattleUnitView battleUnit, State state = null)
        {
            if (state == null || !IsAvailable(state.GetName()))
            {
                battleUnit.State = CreateDefaultState();

                return;
            }

            battleUnit.State = state;
        }

        public States GetName()
        {
            return Name;
        }

        public bool IsAvailable(States stateName)
        {
            return AvailableStates.Contains(stateName);
        }

        public abstract State CreateDefaultState();
    }
}

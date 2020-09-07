using System;
using System.Collections.Generic;
using System.Text;
using Views;
using Models;

namespace Patterns.TemplateMethod
{
    public abstract class TurnActionHandler

    {
        public abstract void PrintMessage(TurnAction action);
        public abstract void HandleChanges(MapView map, TurnAction action);
        

        public void HandleTurnAction(MapView map, TurnAction action)
        {
            PrintMessage(action);
            HandleChanges(map, action);
        }
    }
}

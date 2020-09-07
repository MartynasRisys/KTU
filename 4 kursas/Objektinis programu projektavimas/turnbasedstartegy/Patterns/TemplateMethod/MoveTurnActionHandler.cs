using System;
using Views;
using Models;
using System.Drawing;

namespace Patterns.TemplateMethod
{
    public class MoveTurnActionHandler : TurnActionHandler

    {
        public override void PrintMessage(TurnAction action)
        {
            Console.WriteLine("BattleUnit moved from " + action.X1 + ";" + action.Y1 + " to " + action.X2 + ";" + action.Y2);
        }
        public override void HandleChanges(MapView map, TurnAction action)
        {
            map.setMapUnit(new Point(action.X2, action.Y2), map.getMapUnit(new Point(action.X1, action.Y1)));
            map.setMapUnit(new Point(action.X1, action.Y1), null);
        }
    }
}

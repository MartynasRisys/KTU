using System;
using Views;
using Models;
using System.Drawing;

namespace Patterns.TemplateMethod
{
    public class AttackTurnActionHandler : TurnActionHandler

    {
        public override void PrintMessage(TurnAction action)
        {
            Console.WriteLine("BattleUnit (" + action.X1 + ";" + action.Y1 + ") attacked BattleUnit (" + action.X2 + ";" + action.Y2 + ")");
        }
        public override void HandleChanges(MapView map, TurnAction action)
        {
            BattleUnitView ally = map.getMapUnit(new Point(action.X1, action.Y1)) as BattleUnitView;
            BattleUnitView enemy = map.getMapUnit(new Point(action.X2, action.Y2)) as BattleUnitView;

            if (enemy.getType() == BattleUnitTypeEnum.Circle)
            {
                enemy.takeDamage(ally.DamageVsCircle);
            }
            if (enemy.getType() == BattleUnitTypeEnum.Square)
            {
                enemy.takeDamage(ally.DamageVsSquare);
            }
            if (enemy.getType() == BattleUnitTypeEnum.Triangle)
            {
                enemy.takeDamage(ally.DamageVsTriangle);
            }
            map.setMapUnit(new Point(action.X2, action.Y2), enemy);
        }
    }
}

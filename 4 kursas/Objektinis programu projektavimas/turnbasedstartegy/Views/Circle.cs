using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Views
{
    public class CircleView : BattleUnitView
    {
        public CircleView(long playerId, int x, int y) : base(playerId, x, y)
        {
            DamageVsSquare = 30;
            DamageVsTriangle = 20;
            DamageVsCircle = 10;
        }

        public override BattleUnitTypeEnum getType()
        {
            return BattleUnitTypeEnum.Circle;
        }

        public override BattleUnitView Clone()
        {
            return (BattleUnitView)this.MemberwiseClone();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Views
{
    public class SquareView : BattleUnitView
    {
        public SquareView(long playerId, int x, int y) : base(playerId, x, y)
        {
            DamageVsSquare = 15;
            DamageVsTriangle = 10;
            DamageVsCircle = 5;
        }

        public override BattleUnitTypeEnum getType()
        {
            return BattleUnitTypeEnum.Square;
        }

        public override BattleUnitView Clone()
        {
            return (BattleUnitView)this.MemberwiseClone();
        }
    }
}

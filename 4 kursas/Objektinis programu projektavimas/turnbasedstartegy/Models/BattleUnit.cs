using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public enum BattleUnitTypeEnum { Square = 0, Triangle = 1, Circle = 2 };

    public class BattleUnit
    {

        public long Id { get; set; }
        public long MapId { get; set; }
        public long PlayerId { get; set; }
        public BattleUnitTypeEnum BattleUnitType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int DamageVsSquare { get; set; }
        public int DamageVsTriangle { get; set; }
        public int DamageVsCircle { get; set; }
        public bool SpecialAbilityUsed { get; set; }

        public BattleUnit()
        {
            SpecialAbilityUsed = false;
        }

        public BattleUnit(long mapId, long playerId, int battleUnitTypeIndex, int x, int y, int damageVsSquare, int damageVsTriangle, int damageVsCircle, bool specialAbilityUsed)
        {
            MapId = mapId;
            PlayerId = playerId;

            switch (battleUnitTypeIndex)
            {
                case 0:
                    BattleUnitType = BattleUnitTypeEnum.Square;
                    break;
                case 1:
                    BattleUnitType = BattleUnitTypeEnum.Triangle;
                    break;
                case 2:
                    BattleUnitType = BattleUnitTypeEnum.Circle;
                    break;
            }             

            X = x;
            Y = y;
            DamageVsSquare = damageVsSquare;
            DamageVsTriangle = damageVsTriangle;
            DamageVsCircle = damageVsCircle;
            SpecialAbilityUsed = specialAbilityUsed;
        }
    }
}

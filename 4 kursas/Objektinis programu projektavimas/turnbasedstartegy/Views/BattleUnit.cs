using Models;
using Views.StatePattern;

namespace Views
{
    public abstract class BattleUnitView : IMapUnitView
    {
        public MapUnitTypeEnum Type { get; set; }
        public long PlayerId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int DamageVsSquare { get; set; }
        public int DamageVsTriangle { get; set; }
        public int DamageVsCircle { get; set; }
        public bool SpecialAbilityUsed { get; set; }
        public bool TurnUsed { get; set; }
        public int Health { get; set; }

        private State _state;

        public BattleUnitView(State state = null)
        {
            Type = MapUnitTypeEnum.BattleUnit;
            SpecialAbilityUsed = false;
            TurnUsed = false;
            State = state ?? new Waiting();
        }

        public BattleUnitView(long playerId, int x, int y, State state = null)
        {
            Type = MapUnitTypeEnum.BattleUnit;
            PlayerId = playerId;
            X = x;
            Y = y;
            DamageVsSquare = 0;
            DamageVsTriangle = 0;
            DamageVsCircle = 0;
            Health = 100;
            SpecialAbilityUsed = false;
            TurnUsed = false;
            State = state ?? new Waiting();
        }

        public void takeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }

        public abstract BattleUnitTypeEnum getType();
        public abstract BattleUnitView Clone();

        public State State
        {
            get { return _state; }
            set
            {
                _state = value;
            }
        }
    }
}

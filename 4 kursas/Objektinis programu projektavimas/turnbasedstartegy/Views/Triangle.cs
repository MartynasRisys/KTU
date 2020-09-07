using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Views.MementoPattern;

namespace Views
{
    public class TriangleView : BattleUnitView, IOriginator
    {
        private Caretaker Caretaker;

        public TriangleView(long playerId, int x, int y) : base(playerId, x, y)
        {
            DamageVsSquare = 20;
            DamageVsTriangle = 15;
            DamageVsCircle = 10;
            Caretaker = new Caretaker();
        }

        public override BattleUnitTypeEnum getType()
        {
            return BattleUnitTypeEnum.Triangle;
        }

        public override BattleUnitView Clone()
        {
            return (BattleUnitView)this.MemberwiseClone();
        }

        public bool HasAnyMemento()
        {
            return Caretaker.HasAnyMemento();
        }

        public void RestoreLocation()
        {
            if (!HasAnyMemento())
            {
                return;
            }

            INarrow narrowMemento = Caretaker.GetMemento();
            IWide memento = narrowMemento as IWide;

            RestoreState(memento);
        }

        public string CreateMemento()
        {
            Caretaker.AddMemento(new Memento(this));

            return $"Created memento at {GetX()}, {GetY()}";
        }

        void RestoreState(IWide memento)
        {
            X = memento.GetX();
            Y = memento.GetY();
        }

        public int GetY()
        {
            return Y;
        }

        public int GetX()
        {
            return X;
        }
    }
}

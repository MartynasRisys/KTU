namespace Views.MementoPattern
{
    public class Memento : IWide
    {
        private int X;
        private int Y;

        public Memento(IOriginator originator)
        {
            X = originator.GetX();
            Y = originator.GetY();
        }

        public int GetX()
        {
            return X;
        }

        public int GetY()
        {
            return Y;
        }
    }
}

using System.Collections.Generic;

namespace Views.MementoPattern
{
    public class Caretaker
    {
        private Stack<INarrow> mementoList;

        public Caretaker()
        {
            mementoList = new Stack<INarrow>();
        }

        public void AddMemento(INarrow memento)
        {
            mementoList.Push(memento);
        }

        public INarrow GetMemento()
        {
            return mementoList.Pop();
        }

        public bool HasAnyMemento()
        {
            return mementoList.Count > 0;
        }
    }
}

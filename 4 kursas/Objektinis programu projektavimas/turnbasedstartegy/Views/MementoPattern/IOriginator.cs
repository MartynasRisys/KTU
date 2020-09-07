namespace Views.MementoPattern
{
    public interface IOriginator
    {
        string CreateMemento();

        int GetY();
        int GetX();
    }
}

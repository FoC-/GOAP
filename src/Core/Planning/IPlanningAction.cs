namespace Core.Planning
{
    public interface IPlanningAction<T>
    {
        string Name { get; }
        bool CanExecute(T state);
        T Execute(T state);
    }
}
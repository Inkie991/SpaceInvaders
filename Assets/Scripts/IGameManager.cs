public interface IGameManager
{
    public ManagerStatus Status { get; }

    // It should be called at the start of game to initialize a manager
    void Startup();
}

public enum ManagerStatus
{
    Shutdown,
    Initializing,
    Started
}

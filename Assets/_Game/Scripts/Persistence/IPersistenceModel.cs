public interface IPersistenceModel
{
    GameSessionData Data { get; }
    
    void Flush ();
}
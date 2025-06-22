public interface IPersistenceModel
{
    void Flush ();
    GameSessionData Load ();
    void ClearSave ();
}
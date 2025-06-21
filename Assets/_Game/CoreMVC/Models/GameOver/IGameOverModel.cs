public interface IGameOverModel
{
    int Lives { get; }
    int Score { get; }
    int HighScore { get; }

    void RegisterHighScore ();
    void RestartGame ();
    void ReturnToMenu ();
}
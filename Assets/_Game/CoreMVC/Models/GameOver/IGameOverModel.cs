public interface IGameOverModel
{
    int Lives { get; }
    int Score { get; }
    int HighScore { get; }

    void RestartGame ();
    void ReturnToMenu ();
}
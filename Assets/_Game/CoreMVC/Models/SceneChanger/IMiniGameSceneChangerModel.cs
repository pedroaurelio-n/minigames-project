public interface IMiniGameSceneChangerModel : ISceneChangerModel
{
    void ChangeToNewMiniGame (MiniGameType type);
    void ChangeToMainMenu ();
}
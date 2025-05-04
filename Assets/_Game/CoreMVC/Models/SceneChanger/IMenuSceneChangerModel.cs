public interface IMenuSceneChangerModel : ISceneChangerModel
{
    void ChangeToNewMiniGame ();
    void ChangeToDesiredMiniGame (int index);
    void ChangeToMainMenu ();
}
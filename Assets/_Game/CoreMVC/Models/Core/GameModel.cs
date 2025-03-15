using UnityEngine;

public class GameModel : IGameModel
{
    public IMouseInputModel MouseInputModel { get; }
    
    public ISceneChangerModel SceneChangerModel { get; }
    
    public Camera MainCamera { get; private set; }

    public GameModel (
        IMouseInputModel mouseInputModel,
        ISceneChangerModel sceneChangerModel
    )
    {
        MouseInputModel = mouseInputModel;
        SceneChangerModel = sceneChangerModel;
    }

    public void Initialize ()
    {
        MainCamera = Camera.main;
        MouseInputModel.SetMainCamera(MainCamera);
    }
}
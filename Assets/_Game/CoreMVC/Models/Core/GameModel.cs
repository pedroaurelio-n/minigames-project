using UnityEngine;

public class GameModel : IGameModel
{
    // public IMouseInputModel MouseInputModel { get; }
    public ITouchInputModel TouchInputModel { get; }
    public IDragModel DragModel { get; }
    
    public ISceneChangerModel SceneChangerModel { get; }
    
    public Camera MainCamera { get; private set; }

    public GameModel (
        // IMouseInputModel mouseInputModel,
        ITouchInputModel touchInputModel,
        IDragModel dragModel,
        ISceneChangerModel sceneChangerModel
    )
    {
        // MouseInputModel = mouseInputModel;
        TouchInputModel = touchInputModel;
        DragModel = dragModel;
        SceneChangerModel = sceneChangerModel;
    }

    public void Initialize ()
    {
        MainCamera = Camera.main;
        // MouseInputModel.SetMainCamera(MainCamera);
        TouchInputModel.SetMainCamera(MainCamera);
        DragModel.Initialize();
    }
}
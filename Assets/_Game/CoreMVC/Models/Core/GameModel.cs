using UnityEngine;

public class GameModel : IGameModel
{
    // public IMouseInputModel MouseInputModel { get; }
    public ITouchInputModel TouchInputModel { get; }
    public IDragModel DragModel { get; }
    public IPressModel PressModel { get; }
    
    public ISceneChangerModel SceneChangerModel { get; }
    
    public IMiniGameTimerModel MiniGameTimerModel { get; }
    public IMiniGameModelFactory MiniGameModelFactory { get; }
    public IMiniGameManagerModel MiniGameManagerModel { get; }

    public Camera MainCamera { get; private set; }

    public GameModel (
        // IMouseInputModel mouseInputModel,
        ITouchInputModel touchInputModel,
        IDragModel dragModel,
        IPressModel pressModel,
        ISceneChangerModel sceneChangerModel,
        IMiniGameTimerModel miniGameTimerModel,
        IMiniGameModelFactory miniGameModelFactory,
        IMiniGameManagerModel miniGameManagerModel
    )
    {
        // MouseInputModel = mouseInputModel;
        TouchInputModel = touchInputModel;
        DragModel = dragModel;
        PressModel = pressModel;
        SceneChangerModel = sceneChangerModel;
        MiniGameTimerModel = miniGameTimerModel;
        MiniGameModelFactory = miniGameModelFactory;
        MiniGameManagerModel = miniGameManagerModel;
    }

    public void Initialize ()
    {
        MainCamera = Camera.main;
        // MouseInputModel.SetMainCamera(MainCamera);
        TouchInputModel.SetMainCamera(MainCamera);
        DragModel.Initialize();
        PressModel.Initialize();
        
        MiniGameTimerModel.Initialize();
        MiniGameManagerModel.Initialize();
    }

    public void LateInitialize ()
    {
        MiniGameManagerModel.LateInitialize();
    }

    public void Dispose () { }
}
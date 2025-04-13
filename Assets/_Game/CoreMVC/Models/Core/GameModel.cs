public class GameModel : IGameModel
{
    // public IMouseInputModel MouseInputModel { get; }
    public ITouchInputModel TouchInputModel { get; }
    public IDragModel DragModel { get; }
    public IPressModel PressModel { get; }
    public ICameraMoveModel CameraMoveModel { get; }
    
    public ISceneChangerModel SceneChangerModel { get; }
    
    public IMiniGameTimerModel MiniGameTimerModel { get; }
    public IMiniGameModelFactory MiniGameModelFactory { get; }
    public IMiniGameManagerModel MiniGameManagerModel { get; }

    public GameModel (
        // IMouseInputModel mouseInputModel,
        ITouchInputModel touchInputModel,
        IDragModel dragModel,
        IPressModel pressModel,
        ICameraMoveModel cameraMoveModel,
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
        CameraMoveModel = cameraMoveModel;
        SceneChangerModel = sceneChangerModel;
        MiniGameTimerModel = miniGameTimerModel;
        MiniGameModelFactory = miniGameModelFactory;
        MiniGameManagerModel = miniGameManagerModel;
    }

    public void Initialize ()
    {
        // MouseInputModel.SetMainCamera(MainCamera);
        DragModel.Initialize();
        PressModel.Initialize();
        CameraMoveModel.Initialize();
        
        MiniGameTimerModel.Initialize();
        MiniGameManagerModel.Initialize();
    }

    public void LateInitialize ()
    {
        MiniGameManagerModel.LateInitialize();
    }

    public void Dispose()
    {
        //TODO pedro: leave empty after separating scopes
        DragModel.Dispose();
        PressModel.Dispose();
        CameraMoveModel.Dispose();
        MiniGameTimerModel.Dispose();
        MiniGameManagerModel.Dispose();
    }
}
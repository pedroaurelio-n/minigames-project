public class SceneModel : ISceneModel
{
    // public IMouseInputModel MouseInputModel { get; }
    public ITouchInputModel TouchInputModel { get; }
    public IDragModel DragModel { get; }
    public IPressModel PressModel { get; }
    public ICameraMoveModel CameraMoveModel { get; }
    
    public IMiniGameSceneChangerModel MiniGameSceneChangerModel { get; }
    public IMiniGameDifficultyModel MiniGameDifficultyModel { get; }
    public IMiniGameTimerModel MiniGameTimerModel { get; }
    public IMiniGameModelFactory MiniGameModelFactory { get; }
    public IMiniGameManagerModel MiniGameManagerModel { get; }
    public IMiniGameSelectorModel MiniGameSelectorModel { get; }

    public SceneModel (
        // IMouseInputModel mouseInputModel,
        ITouchInputModel touchInputModel,
        IDragModel dragModel,
        IPressModel pressModel,
        ICameraMoveModel cameraMoveModel,
        IMiniGameSceneChangerModel miniGameSceneChangerModel,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel,
        IMiniGameModelFactory miniGameModelFactory,
        IMiniGameManagerModel miniGameManagerModel,
        IMiniGameSelectorModel miniGameSelectorModel
    )
    {
        // MouseInputModel = mouseInputModel;
        TouchInputModel = touchInputModel;
        DragModel = dragModel;
        PressModel = pressModel;
        CameraMoveModel = cameraMoveModel;
        MiniGameSceneChangerModel = miniGameSceneChangerModel;
        MiniGameDifficultyModel = miniGameDifficultyModel;
        MiniGameTimerModel = miniGameTimerModel;
        MiniGameModelFactory = miniGameModelFactory;
        MiniGameManagerModel = miniGameManagerModel;
        MiniGameSelectorModel = miniGameSelectorModel;
    }

    public void Initialize ()
    {
        // MouseInputModel.SetMainCamera(MainCamera);
        DragModel.Initialize();
        PressModel.Initialize();
        
        MiniGameDifficultyModel.Initialize();
        MiniGameTimerModel.Initialize();
        MiniGameManagerModel.Initialize();
        MiniGameSelectorModel.Initialize();
    }

    public void LateInitialize ()
    {
        MiniGameManagerModel.LateInitialize();
    }

    public void Dispose() { }
}
using System.Collections.Generic;

public class LevelSelectPanelUIController : BaseMainMenuPanelUIController
{
    protected override MainMenuState State => MainMenuState.LevelSelect;
    
    readonly LevelSelectPanelUIView _view;
    readonly FadeToBlackManager _fadeToBlackManager;
    readonly PoolableViewFactory _viewFactory;
    readonly IMiniGameSystemSettings _miniGameSystemSettings;
    readonly IMiniGameSettingsAccessor _miniGameSettingsAccessor;
    readonly IMiniGameSelectorModel _miniGameSelectorModel;
    readonly List<LevelSelectButtonUIView> _buttonUIViews = new();
    
    public LevelSelectPanelUIController (
        IMainMenuModel model,
        LevelSelectPanelUIView view,
        FadeToBlackManager fadeToBlackManager,
        PoolableViewFactory viewFactory,
        IMiniGameSystemSettings miniGameSystemSettings,
        IMiniGameSettingsAccessor miniGameSettingsAccessor,
        IMiniGameSelectorModel miniGameSelectorModel
    ) : base(model)
    {
        _view = view;
        _fadeToBlackManager = fadeToBlackManager;
        _viewFactory = viewFactory;
        _miniGameSystemSettings = miniGameSystemSettings;
        _miniGameSettingsAccessor = miniGameSettingsAccessor;
        _miniGameSelectorModel = miniGameSelectorModel;
    }
    
    public override void Initialize ()
    {
        base.Initialize();
        AddViewListeners();
        
        _viewFactory.SetupPool(_view.LevelSelectButtonPrefab);
    }
    
    protected override void Enable ()
    {
        _view.SetActive(true);

        CreateMissingInstances();
        UpdateInstances();
    }

    protected override void Disable ()
    {
        _view.SetActive(false);
    }

    void CreateMissingInstances ()
    {
        int activeCount = _buttonUIViews.Count;
        int missingCount = _miniGameSelectorModel.ActiveMiniGames.Count - activeCount;
        for (int i = activeCount; i < missingCount; i++)
        {
            LevelSelectButtonUIView buttonUIView = _viewFactory.GetView<LevelSelectButtonUIView>(_view.ButtonContainer);
            buttonUIView.LevelIndex = i;
            buttonUIView.OnClick += HandleLevelButtonClick;
            _buttonUIViews.Add(buttonUIView);
        }
    }

    void UpdateInstances ()
    {
        foreach (LevelSelectButtonUIView buttonUIView in _buttonUIViews)
            buttonUIView.SetNameText(_miniGameSettingsAccessor.GetSettingsByIndex(buttonUIView.LevelIndex).Name);
    }

    void AddViewListeners ()
    {
        _view.OnBackButtonClick += HandleBackButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnBackButtonClick -= HandleBackButtonClick;
    }

    void HandleBackButtonClick ()
    {
        Model.ChangeMainMenuState(MainMenuState.Menu);
    }

    void HandleLevelButtonClick (int levelIndex)
    {
        _fadeToBlackManager.FadeIn(() => Model.SelectLevel(levelIndex), true);
    }
    
    public override void Dispose ()
    {
        foreach (LevelSelectButtonUIView buttonUIView in _buttonUIViews)
            buttonUIView.OnClick -= HandleLevelButtonClick;
        _buttonUIViews.DisposeAndClear();
        
        base.Dispose();
        RemoveViewListeners();
    }
}
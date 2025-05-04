using System.Collections.Generic;

public class LevelSelectPanelUIController
{
    readonly IMainMenuModel _model;
    readonly LevelSelectPanelUIView _view;
    readonly FadeToBlackManager _fadeToBlackManager;
    readonly PoolableViewFactory _viewFactory;
    readonly IMiniGameSystemSettings _miniGameSystemSettings;
    readonly IndexedSettingsProvider<IMiniGameSettings, MiniGameSettings> _miniGameSettingsProvider;
    readonly List<LevelSelectButtonUIView> _buttonUIViews = new();
    
    public LevelSelectPanelUIController (
        IMainMenuModel model,
        LevelSelectPanelUIView view,
        FadeToBlackManager fadeToBlackManager,
        PoolableViewFactory viewFactory,
        IMiniGameSystemSettings miniGameSystemSettings,
        IndexedSettingsProvider<IMiniGameSettings, MiniGameSettings> miniGameSettingsProvider
    )
    {
        _model = model;
        _view = view;
        _fadeToBlackManager = fadeToBlackManager;
        _viewFactory = viewFactory;
        _miniGameSystemSettings = miniGameSystemSettings;
        _miniGameSettingsProvider = miniGameSettingsProvider;
    }
    
    public void Initialize ()
    {
        AddListeners();
        AddViewListeners();
        
        _viewFactory.SetupPool(_view.LevelSelectButtonPrefab);
    }
    
    void Enable ()
    {
        _view.SetActive(true);

        CreateMissingInstances();
        UpdateInstances();
    }

    void Disable ()
    {
        _view.SetActive(false);
    }

    void CreateMissingInstances ()
    {
        int activeCount = _buttonUIViews.Count;
        int missingCount = _miniGameSystemSettings.ActiveMiniGames.Count - activeCount;
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
        {
            buttonUIView.SetNameText(
                _miniGameSettingsProvider.GetSettingsByIndex(buttonUIView.LevelIndex).Instance.Name
            );
        }
    }

    void AddListeners ()
    {
        _model.OnMainMenuStateChanged += HandleMainMenuStateChanged;
    }
    
    void RemoveListeners ()
    {
        _model.OnMainMenuStateChanged -= HandleMainMenuStateChanged;
    }

    void AddViewListeners ()
    {
        _view.OnBackButtonClick += HandleBackButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnBackButtonClick -= HandleBackButtonClick;
    }

    void HandleMainMenuStateChanged (MainMenuState newState)
    {
        if (newState == MainMenuState.LevelSelect)
        {
            Enable();
            return;
        }
        
        Disable();
    }

    void HandleBackButtonClick ()
    {
        _model.ChangeMainMenuState(MainMenuState.Menu);
    }

    void HandleLevelButtonClick (int levelIndex)
    {
        _fadeToBlackManager.FadeIn(() => _model.SelectLevel(levelIndex), true);
    }
    
    public void Dispose ()
    {
        foreach (LevelSelectButtonUIView buttonUIView in _buttonUIViews)
            buttonUIView.OnClick -= HandleLevelButtonClick;
        _buttonUIViews.DisposeAndClear();
        RemoveListeners();
        RemoveViewListeners();
    }
}
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerUIController : IDisposable
{
    readonly ISceneChangerModel _model;
    readonly IMiniGameTimerModel _miniGameTimerModel;
    readonly GameUIView _gameUIView;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;

    SceneChangerUIView _view;

    bool _isChangingScene;

    public SceneChangerUIController (
        ISceneChangerModel model,
        IMiniGameTimerModel miniGameTimerModel,
        GameUIView gameUIView,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _model = model;
        _miniGameTimerModel = miniGameTimerModel;
        _gameUIView = gameUIView;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        InstantiateSceneChangeButton();
    }

    public void Initialize ()
    {
        AddListeners();
        AddViewListeners();
    }

    void InstantiateSceneChangeButton () 
        => _view = GameObject.Instantiate(
            Resources.Load<SceneChangerUIView>("SceneChangerUIView"),
            _gameUIView.ChangeSceneContainer
        );

    void AddListeners ()
    {
        _miniGameTimerModel.OnTimerEnded += HandleTimerEnded;
    }
    
    void RemoveListeners ()
    {
        _miniGameTimerModel.OnTimerEnded -= HandleTimerEnded;
    }

    void AddViewListeners ()
    {
        _view.OnClick += HandleViewClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnClick -= HandleViewClick;
    }

    void HandleTimerEnded () => ChangeScene();

    void HandleViewClick () => ChangeScene();

    void ChangeScene ()
    {
        if (_isChangingScene)
            return;
        
        _isChangingScene = true;
        _gameUIView.FadeToBlackManager.FadeIn(ChangeScene);
        
        void ChangeScene ()
        {
            int newSceneIndex = _gameSessionInfoProvider.CurrentSceneIndex + 1;
            if (newSceneIndex > SceneManager.sceneCountInBuildSettings)
                newSceneIndex = 1;
            
            string newSceneName = SceneManagerUtils.GetSceneNameFromBuildIndex(newSceneIndex);
            _model.ChangeScene(newSceneName);
        }
    }

    public void Dispose ()
    {
        RemoveListeners();
        RemoveViewListeners();
    }
}
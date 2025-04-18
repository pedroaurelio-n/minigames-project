using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class SceneChangerUIController : IDisposable
{
    readonly SceneUIView _sceneUIView;
    readonly MiniGameSceneChangerController _miniGameSceneChangerController;
    readonly DebugOptions _debugOptions;
    
    SceneChangerUIView _view;

    public SceneChangerUIController (
        SceneUIView sceneUIView,
        MiniGameSceneChangerController miniGameSceneChangerController,
        DebugOptions debugOptions
    )
    {
        _sceneUIView = sceneUIView;
        _miniGameSceneChangerController = miniGameSceneChangerController;
        _debugOptions = debugOptions;
    }

    public void Initialize ()
    {
        if (_debugOptions.DebugMode)
            InstantiateSceneChangeButton();
        
        AddViewListeners();
    }
    
    void InstantiateSceneChangeButton () 
        => _view = Object.Instantiate(
            Resources.Load<SceneChangerUIView>("SceneChangerUIView"),
            _sceneUIView.ChangeSceneContainer
        );
    
    void AddViewListeners ()
    {
        if (_view == null)
            return;
        _view.OnClick += HandleViewClick;
    }
    
    void RemoveViewListeners ()
    {
        if (_view == null)
            return;
        _view.OnClick -= HandleViewClick;
    }
    
    void HandleViewClick () => _miniGameSceneChangerController.ChangeSceneClick();

    public void Dispose ()
    {
        RemoveViewListeners();
    }
}
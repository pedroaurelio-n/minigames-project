using System;
using UnityEngine;

public class BaseSceneChangerController : IDisposable
{
    protected readonly ISceneChangerModel Model;
    protected readonly SceneUIView SceneUIView;
    
    protected bool IsChangingScene;

    SceneChangerUIView _view;
    
    public BaseSceneChangerController (
        ISceneChangerModel model,
        SceneUIView sceneUIView
    )
    {
        Model = model;
        SceneUIView = sceneUIView;
        InstantiateSceneChangeButton();
    }

    public void Initialize ()
    {
        AddListeners();
        AddViewListeners();
    }
    
    protected virtual void AddListeners () { }
    
    protected virtual void RemoveListeners () { }
    
    protected virtual void ChangeSceneClick () { }

    void InstantiateSceneChangeButton () 
        => _view = GameObject.Instantiate(
            Resources.Load<SceneChangerUIView>("SceneChangerUIView"),
            SceneUIView.ChangeSceneContainer
        );

    void AddViewListeners ()
    {
        _view.OnClick += HandleViewClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnClick -= HandleViewClick;
    }

    void HandleViewClick () => ChangeSceneClick();

    public void Dispose ()
    {
        RemoveListeners();
        RemoveViewListeners();
    }
}
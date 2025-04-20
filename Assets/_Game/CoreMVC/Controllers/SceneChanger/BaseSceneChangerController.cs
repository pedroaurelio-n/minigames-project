using System;
using UnityEngine;

public class BaseSceneChangerController : IDisposable
{
    protected readonly ISceneChangerModel Model;
    
    protected bool IsChangingScene;
    
    public BaseSceneChangerController (
        ISceneChangerModel model
    )
    {
        Model = model;
    }

    public void Initialize ()
    {
        AddListeners();
    }
    
    public virtual void ChangeSceneClick () { }
    
    protected virtual void AddListeners () { }
    
    protected virtual void RemoveListeners () { }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
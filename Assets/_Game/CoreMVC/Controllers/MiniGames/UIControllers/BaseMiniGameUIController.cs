using System;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class BaseMiniGameUIController : IDisposable
{
    public abstract void Setup(SceneUIView sceneUIView);
    public abstract void Dispose();
}

public class BaseMiniGameUIController<T> : BaseMiniGameUIController where T : MiniGameUIView
{
    public T UIView { get; private set; }
    
    protected SceneUIView SceneUIView { get; private set; }
    
    public override void Setup (SceneUIView sceneUIView)
    {
        SceneUIView = sceneUIView;
        UIView = Object.Instantiate(Resources.Load<T>(typeof(T).Name), SceneUIView.PriorityHUD);
    }

    protected virtual void AddViewListeners () { }
    
    protected virtual void RemoveViewListeners () { }
    
    public override void Dispose ()
    {
    }
}
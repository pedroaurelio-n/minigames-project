using System;

public class BaseMiniGameUIController : IDisposable
{
    protected SceneUIView SceneUIView { get; private set; }
    
    public virtual void Setup (SceneUIView sceneUIView)
    {
        SceneUIView = sceneUIView;
    }
    
    public virtual void Dispose ()
    {
    }
}
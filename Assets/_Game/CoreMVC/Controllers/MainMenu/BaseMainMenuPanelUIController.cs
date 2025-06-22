using System;

public abstract class BaseMainMenuPanelUIController : IDisposable
{
    protected abstract MainMenuState State { get; }
    
    protected readonly IMainMenuModel Model;
    
    protected BaseMainMenuPanelUIController (IMainMenuModel model)
    {
        Model = model;
    }

    public virtual void Initialize ()
    {
        AddListeners();
    }

    protected abstract void Enable ();
    
    protected abstract void Disable ();
    
    void AddListeners ()
    {
        Model.OnMainMenuStateChanged += HandleMainMenuStateChanged;
    }
    
    void RemoveListeners ()
    {
        Model.OnMainMenuStateChanged -= HandleMainMenuStateChanged;
    }
    
    void HandleMainMenuStateChanged (MainMenuState newState)
    {
        if (newState == State)
        {
            Enable();
            return;
        }
        
        Disable();
    }
    
    public virtual void Dispose ()
    {
        RemoveListeners();
    }
}
using System;

public class ButtonMashMiniGameUIController : BaseMiniGameUIController<ButtonMashMiniGameUIView>
{
    public event Action OnLeftButtonClick;
    public event Action OnRightButtonClick;
    
    public override void Setup (SceneUIView sceneUIView)
    {
        base.Setup(sceneUIView);
        AddViewListeners();
    }

    public void SyncView (int current, int milestone)
    {
        UIView.SetLeftButtonText($"{current}/{milestone}");
        UIView.SetRightButtonText($"Click to fail");
    }

    protected override void AddViewListeners ()
    {
        UIView.OnLeftButtonClick += HandleLeftButtonClick;
        UIView.OnRightButtonClick += HandleRightButtonClick;
    }
    
    protected override void RemoveViewListeners ()
    {
        UIView.OnLeftButtonClick -= HandleLeftButtonClick;
        UIView.OnRightButtonClick -= HandleRightButtonClick;
    }
    
    void HandleLeftButtonClick () => OnLeftButtonClick?.Invoke();
    
    void HandleRightButtonClick () => OnRightButtonClick?.Invoke();

    public override void Dispose ()
    {
        RemoveViewListeners();
    }
}
using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ClickMilestoneMiniGameUIController : BaseMiniGameUIController
{
    public event Action OnLeftButtonClick;
    public event Action OnRightButtonClick;
    
    ClickMilestoneMiniGameUIView _uiView;
    
    public override void Setup (SceneUIView sceneUIView)
    {
        base.Setup(sceneUIView);

        _uiView = Object.Instantiate(
            Resources.Load<ClickMilestoneMiniGameUIView>("ClickMilestoneMiniGameUIView"),
            SceneUIView.PriorityHUD
        );
        AddViewListeners();
    }

    public void SyncView (int current, int milestone)
    {
        _uiView.SetLeftButtonText($"{current}/{milestone}");
        _uiView.SetRightButtonText($"Click to fail");
    }

    void AddViewListeners ()
    {
        _uiView.OnLeftButtonClick += HandleLeftButtonClick;
        _uiView.OnRightButtonClick += HandleRightButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        _uiView.OnLeftButtonClick -= HandleLeftButtonClick;
        _uiView.OnRightButtonClick -= HandleRightButtonClick;
    }
    
    void HandleLeftButtonClick () => OnLeftButtonClick?.Invoke();
    
    void HandleRightButtonClick () => OnRightButtonClick?.Invoke();

    public override void Dispose ()
    {
        RemoveViewListeners();
    }
}
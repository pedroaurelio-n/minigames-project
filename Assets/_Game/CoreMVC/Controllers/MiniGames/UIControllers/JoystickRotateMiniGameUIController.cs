using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class JoystickRotateMiniGameUIController : BaseMiniGameUIController
{
    public event Action<Vector2> OnJoystickDirectionUpdated;
    
    JoystickRotateMiniGameUIView _uiView;
    
    public override void Setup (SceneUIView sceneUIView)
    {
        base.Setup(sceneUIView);

        _uiView = Object.Instantiate(
            Resources.Load<JoystickRotateMiniGameUIView>("JoystickRotateMiniGameUIView"),
            SceneUIView.PriorityHUD
        );
        AddViewListeners();
    }

    void AddViewListeners ()
    {
        _uiView.OnJoystickDirectionUpdated += HandleJoystickDirectionUpdated;
    }
    
    void RemoveViewListeners ()
    {
        _uiView.OnJoystickDirectionUpdated -= HandleJoystickDirectionUpdated;
    }
    
    void HandleJoystickDirectionUpdated (Vector2 direction) => OnJoystickDirectionUpdated?.Invoke(direction);

    public override void Dispose ()
    {
        RemoveViewListeners();
    }
}
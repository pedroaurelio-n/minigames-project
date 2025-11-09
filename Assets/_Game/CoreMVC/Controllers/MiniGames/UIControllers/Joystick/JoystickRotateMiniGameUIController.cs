using System;
using UnityEngine;

public class JoystickRotateMiniGameUIController : BaseMiniGameUIController<JoystickRotateMiniGameUIView>
{
    public event Action<Vector2> OnJoystickDirectionUpdated;
    
    public override void Setup (SceneUIView sceneUIView)
    {
        base.Setup(sceneUIView);
        AddViewListeners();
    }

    protected override void AddViewListeners ()
    {
        UIView.OnJoystickDirectionUpdated += HandleJoystickDirectionUpdated;
    }
    
    protected override void RemoveViewListeners ()
    {
        UIView.OnJoystickDirectionUpdated -= HandleJoystickDirectionUpdated;
    }
    
    void HandleJoystickDirectionUpdated (Vector2 direction) => OnJoystickDirectionUpdated?.Invoke(direction);

    public override void Dispose ()
    {
        RemoveViewListeners();
    }
}
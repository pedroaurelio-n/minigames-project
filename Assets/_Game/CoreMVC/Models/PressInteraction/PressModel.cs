using System;
using UnityEngine;

public class PressModel : IPressModel
{
    public event Action<IPressable, Vector2> OnTapPerformed;
    
    readonly ITouchInputModel _touchInputModel;
    readonly IPhysicsProvider _physicsProvider;
    readonly LayerMaskOptions _layerMaskOptions;

    public PressModel (
        ITouchInputModel touchInputModel,
        IPhysicsProvider physicsProvider,
        LayerMaskOptions layerMaskOptions
    )
    {
        _touchInputModel = touchInputModel;
        _physicsProvider = physicsProvider;
        _layerMaskOptions = layerMaskOptions;
    }
    
    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        _touchInputModel.OnTapPerformed += HandleTapPerformed;
    }

    void RemoveListeners ()
    {
        _touchInputModel.OnTapPerformed -= HandleTapPerformed;
    }

    void HandleTapPerformed (Vector2 touchPosition)
    {
        if (_touchInputModel.MainCamera == null)
            return;
        
        Ray ray = _touchInputModel.MainCamera.ScreenPointToRay(touchPosition);

        if (!_physicsProvider.Raycast(ray, _layerMaskOptions.InteractableLayers, out RaycastHit hit))
            return;
            
        IPressable pressable = hit.collider.GetComponent<IPressable>();
            
        if (pressable == null)
            return;
        
        OnTapPerformed?.Invoke(pressable, touchPosition);
        pressable.OnTapped();
        
        DebugUtils.Log($"Tap performed for {pressable.Name}, at {touchPosition}");
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
using System;
using UnityEngine;

public class PressModel : IPressModel
{
    public event Action<ITappable, Vector2> OnTapPerformed;
    public event Action<ILongPressable, Vector2> OnLongPressBegan;
    public event Action<ILongPressable, Vector2> OnLongPressCancelled;
    public event Action<ILongPressable, Vector2, float> OnLongPressEnded;
    
    readonly ITouchInputModel _touchInputModel;
    readonly IInputRaycastProvider _inputRaycastProvider;
    readonly LayerMaskOptions _layerMaskOptions;

    public PressModel (
        ITouchInputModel touchInputModel,
        IInputRaycastProvider inputRaycastProvider,
        LayerMaskOptions layerMaskOptions
    )
    {
        _touchInputModel = touchInputModel;
        _inputRaycastProvider = inputRaycastProvider;
        _layerMaskOptions = layerMaskOptions;
    }
    
    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        _touchInputModel.OnTapPerformed += HandleTapPerformed;
        _touchInputModel.OnLongPressStarted += HandleLongPressStarted;
        _touchInputModel.OnLongPressCancelled += HandleLongPressCancelled;
        _touchInputModel.OnLongPressEnded += HandleLongPressEnded;
    }

    void RemoveListeners ()
    {
        _touchInputModel.OnTapPerformed -= HandleTapPerformed;
        _touchInputModel.OnLongPressStarted -= HandleLongPressStarted;
        _touchInputModel.OnLongPressCancelled -= HandleLongPressCancelled;
        _touchInputModel.OnLongPressEnded -= HandleLongPressEnded;
    }

    void HandleTapPerformed (Vector2 touchPosition)
    {
        if (!_inputRaycastProvider.TryGetHitComponent(
                touchPosition,
                _layerMaskOptions.InteractableLayers,
                out ITappable tappable))
            return;
        
        tappable.OnTapped();
        OnTapPerformed?.Invoke(tappable, touchPosition);
        
        DebugUtils.Log($"Tap performed for {tappable.Name}, at {touchPosition}");
    }
    
    void HandleLongPressStarted (Vector2 touchPosition)
    {
        if (!_inputRaycastProvider.TryGetHitComponent(
                touchPosition,
                _layerMaskOptions.InteractableLayers,
                out ILongPressable pressable))
            return;
        
        pressable.OnLongPressBegan();
        OnLongPressBegan?.Invoke(pressable, touchPosition);
        
        DebugUtils.Log($"Long press began for {pressable.Name}, at {touchPosition}");
    }
    
    void HandleLongPressCancelled (Vector2 touchPosition)
    {
        if (!_inputRaycastProvider.TryGetHitComponent(
                touchPosition,
                _layerMaskOptions.InteractableLayers,
                out ILongPressable pressable))
            return;
        
        pressable.OnLongPressCancelled();
        OnLongPressCancelled?.Invoke(pressable, touchPosition);
        
        DebugUtils.Log($"Long press cancelled for {pressable.Name}, at {touchPosition}");
    }
    
    void HandleLongPressEnded (Vector2 touchPosition, float duration)
    {
        if (!_inputRaycastProvider.TryGetHitComponent(
                touchPosition,
                _layerMaskOptions.InteractableLayers,
                out ILongPressable pressable))
            return;
        
        pressable.OnLongPressEnded();
        OnLongPressEnded?.Invoke(pressable, touchPosition, duration);
        
        DebugUtils.Log($"Long press began for {pressable.Name}, at {touchPosition}, for {duration} seconds.");
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
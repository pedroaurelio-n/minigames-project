using System;
using UnityEngine;

public class JoystickAimTargetView : PoolableView
{
    public event Action<JoystickAimTargetView, JoystickAimProjectileView> OnTargetHit;

    void OnTriggerEnter (Collider other)
    {
        if (!other.TryGetComponent(out JoystickAimProjectileView projectile))
            return;
        OnTargetHit?.Invoke(this, projectile);
    }
}
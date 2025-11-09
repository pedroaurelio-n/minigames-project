using UnityEngine;

public class JoystickAimProjectileView : PoolableView
{
    [SerializeField] Rigidbody _rb;
    
    public void Setup (Vector3 direction)
    {
        _rb.linearVelocity = direction;
    }
}
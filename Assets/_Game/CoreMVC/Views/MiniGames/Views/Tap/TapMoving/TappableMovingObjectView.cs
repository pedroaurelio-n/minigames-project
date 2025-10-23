using UnityEngine;

public class TappableMovingObjectView : PoolableView, ITappable
{
    [SerializeField] Rigidbody rb;
    
    public string Name => gameObject.name;

    Vector2 _direction;
    float _delayTimer;
    bool _active;
    
    public void Setup (Vector2 direction, float delay)
    {
        _active = false;
        rb.linearVelocity = Vector3.zero;
        
        _direction = direction;
        _delayTimer = delay;
    }

    public void EvaluateActivation ()
    {
        if (_active)
            return;
        
        if (!_active && _delayTimer > 0f)
        {
            _delayTimer -= Time.deltaTime;
            return;
        }

        _active = true;
        rb.linearVelocity = _direction;
    }

    public void OnTapped ()
    {
    }
}
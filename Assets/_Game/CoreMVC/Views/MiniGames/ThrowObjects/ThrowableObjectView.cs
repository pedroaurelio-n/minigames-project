using UnityEngine;

public class ThrowableObjectView : PoolableView
{
    [SerializeField] Rigidbody rigidbody;

    public void Setup (
        Vector3 initialPosition,
        Quaternion initialRotation,
        Vector3 force
    )
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
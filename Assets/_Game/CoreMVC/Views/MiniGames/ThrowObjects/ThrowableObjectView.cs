using UnityEngine;

public class ThrowableObjectView : PoolableView
{
    [SerializeField] Rigidbody rb;

    public void Setup (
        Vector3 initialPosition,
        Quaternion initialRotation,
        Vector3 force
    )
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        rb.AddForce(force, ForceMode.VelocityChange);
    }
}
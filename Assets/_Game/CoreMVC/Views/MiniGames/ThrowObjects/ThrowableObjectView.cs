using UnityEngine;

public class ThrowableObjectView : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;

    public void Throw (Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
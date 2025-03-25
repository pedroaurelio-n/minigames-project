using System;
using UnityEngine;

public class ThrowableContainerView : MonoBehaviour
{
    public event Action OnThrowableEnter;

    void OnTriggerEnter (Collider other)
    {
        if (!other.TryGetComponent<ThrowableObjectView>(out _))
            return;
        OnThrowableEnter?.Invoke();
    }
}
using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
{
    public Coroutine DeployCoroutine (IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void KillCoroutine (Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}
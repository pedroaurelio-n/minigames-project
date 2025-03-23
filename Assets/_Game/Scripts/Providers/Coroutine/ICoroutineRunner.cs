using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    Coroutine DeployCoroutine (IEnumerator routine);
    void KillCoroutine (Coroutine coroutine);
}
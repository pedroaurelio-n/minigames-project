using System;
using System.Collections;
using UnityEngine;

public class UniqueCoroutine : IDisposable
{
    Coroutine _currentCoroutine;
    
    readonly ICoroutineRunner _runner;

    public UniqueCoroutine (ICoroutineRunner runner)
    {
        _runner = runner;
    }

    public void Start (IEnumerator routine)
    {
        if (_currentCoroutine != null)
            return;
        _currentCoroutine = _runner.DeployCoroutine(RunRoutine(routine));
    }

    public void Stop ()
    {
        if (_currentCoroutine == null)
            return;
        _runner.KillCoroutine(_currentCoroutine);
        _currentCoroutine = null;
    }

    IEnumerator RunRoutine (IEnumerator routine)
    {
        yield return routine;
        _currentCoroutine = null;
    }

    public void Dispose ()
    {
        Stop();
    }
}
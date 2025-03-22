using System;
using UnityEngine;

public static class DebugUtils
{
    public static void Log (string message)
    {
        if (!GameGlobalOptions.Instance.DebugOptions.EnableLogging)
            return;
        Debug.Log(message);
    }

    public static void LogWarning (string message)
    {
        if (!GameGlobalOptions.Instance.DebugOptions.EnableLogging)
            return;
        Debug.LogWarning(message);
    }

    public static void LogError (string message)
    {
        if (!GameGlobalOptions.Instance.DebugOptions.EnableLogging)
            return;
        Debug.LogError(message);
    }

    public static void LogException (string message, Func<string, Exception> exceptionFactory = null)
    {
        if (!GameGlobalOptions.Instance.DebugOptions.EnableLogging)
            return;
        Exception exception = exceptionFactory == null ? new Exception(message) : exceptionFactory(message);
        Debug.LogException(exception);
    }
}
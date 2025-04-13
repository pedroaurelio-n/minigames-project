using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class DebugUtils
{
    public static void Log (
        string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        if (!GameGlobalOptions.Instance.DebugOptions.EnableLogging)
            return;
        Debug.Log(Format(message, memberName, sourceFilePath, sourceLineNumber));
    }

    public static void LogWarning (
        string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        if (!GameGlobalOptions.Instance.DebugOptions.EnableLogging)
            return;
        Debug.LogWarning(Format(message, memberName, sourceFilePath, sourceLineNumber));
    }

    public static void LogError (
        string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        if (!GameGlobalOptions.Instance.DebugOptions.EnableLogging)
            return;
        Debug.LogError(Format(message, memberName, sourceFilePath, sourceLineNumber));
    }

    public static void LogException (
        string message,
        Func<string, Exception> exceptionFactory = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        if (!GameGlobalOptions.Instance.DebugOptions.EnableLogging)
            return;
        string formattedMessage = Format(message, memberName, sourceFilePath, sourceLineNumber);
        Exception exception = exceptionFactory == null
            ? new Exception(formattedMessage)
            : exceptionFactory(formattedMessage);
        Debug.LogException(exception);
    }
    
    static string Format (string message, string memberName, string filePath, int lineNumber)
    {
        string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
        return $"{message} | [{className}.{memberName}:{lineNumber}] ";
    }
}
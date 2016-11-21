using System;
using UnityEngine;
using System.IO;

public static class Logger
{
    public static void Log(object message)
    {
#if DEBUG || UNITY_EDITOR
        Debug.Log(message.ToString());
#endif
    }

    public static void LogError(object message)
    {
#if DEBUG || UNITY_EDITOR
        Debug.LogError(message.ToString());
#endif
    }

    public static void LogWarning(object message)
    {
#if DEBUG || UNITY_EDITOR
        Debug.LogWarning(message.ToString());
#endif
    }

    public static void LogGreen(object message)
    {
        Logger.Log("<color=green>" + message + "</color>");
    }

    public static void LogRed(object message)
    {
        Logger.Log("<color=red>" + message + "</color>");
    }

    public static void LogYellow(object message)
    {
        Logger.Log("<color=yellow>" + message + "</color>");
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class Test : MonoBehaviour
    {
        private enum LoggingLevel
        {
            Verbose,
            Information,
            Warning,
            Error,
            Assertion,
            Exception,
        }
        private static string GetLogMessage(LoggingLevel level, string message) =>
            level switch
            {
                LoggingLevel.Verbose => $"<color=gray>{message}</color>",
                LoggingLevel.Information => $"<color=chartreuse>{message}</color>",
                LoggingLevel.Warning => $"<color=yellow>{message}</color>",
                LoggingLevel.Error => $"<color=crimson>{message}</color>",
                LoggingLevel.Assertion => $"<color=purple>{message}</color>",
                LoggingLevel.Exception => $"<color=magenta>{message}</color>",
                _ => throw new System.ArgumentException("invalid enum value", nameof(level))
            };

        private void OnEnable()
        {
            Application.logMessageReceived += logMessageReceived;
        }        
        private void OnDisable()
        {
            Application.logMessageReceived -= logMessageReceived;
        }
        private void logMessageReceived(string condition, string stackTrace, LogType type)
        {
            var messageColor = string.Empty;
            switch (type)
            {
                case LogType.Log:
                    messageColor = GetLogMessage(LoggingLevel.Verbose, condition);
                    break;

                case LogType.Warning:
                    messageColor = GetLogMessage(LoggingLevel.Warning, condition);
                    break;

                case LogType.Error:
                    messageColor = GetLogMessage(LoggingLevel.Error, condition);
                    break;

                default:
                    break;
            }
        }


        // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            UnityEngine.Debug.Log("Log");

            yield return new WaitForSeconds(1);
            UnityEngine.Debug.LogWarning("Warning");

            yield return new WaitForSeconds(1);
            UnityEngine.Debug.LogError("Error");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

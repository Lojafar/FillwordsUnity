using UnityEngine;
namespace FillWords.Utils.Debugers
{
    class UnityDebuger : IDebuger
    {
        public void Log(object message, LogType logType)
        {
            switch (logType)
            {
                case LogType.Message:
                    Debug.Log(message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogType.Error:
                    Debug.LogError(message);
                    break;
                default:
                    Debug.Log("The LogType case isn't setted, but message is " + message);
                    break;
            }
        }
    }
}

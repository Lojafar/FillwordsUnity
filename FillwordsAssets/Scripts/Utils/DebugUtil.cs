using FillWords.Utils.Debugers;
namespace FillWords.Utils
{
    public static class DebugUtil
    {
        readonly static IDebuger debuger = new UnityDebuger();
        public static void Log(object message, LogType logType = LogType.Message)
        {
            debuger.Log(message, logType);
        }
    }
}

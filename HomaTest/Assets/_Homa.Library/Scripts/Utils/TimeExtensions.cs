using UnityEngine;

namespace _Homa.Library.Scripts.Utils
{
    public static class TimeExtensions
    {
        /// <summary>
        /// Take seconds to minute:seconds format.
        /// </summary>
        /// <returns></returns>
        public static string SecondsToFormattedTimeEM(this float timeInSeconds)
        {
            return SecondsToFormattedTime(timeInSeconds);
        }

        /// <summary>
        /// Take seconds to minute:seconds format.
        /// </summary>
        /// <param name="timeInSeconds">Time in seconds of the timer</param>
        /// <returns></returns>
        public static string SecondsToFormattedTime(float timeInSeconds)
        {
            int min = Mathf.FloorToInt(timeInSeconds / 60);
            int sec = Mathf.FloorToInt(timeInSeconds % 60);
            return min.ToString("00") + ":" + sec.ToString("00");
        }
    }
}
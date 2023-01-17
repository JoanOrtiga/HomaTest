using UnityEditor;
using UnityEngine;

namespace _Homa.Library.Editor
{
    public class ResetPlayerPrefsTool
    {
        [MenuItem("Tools/Reset player prefs")]
        public static void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}

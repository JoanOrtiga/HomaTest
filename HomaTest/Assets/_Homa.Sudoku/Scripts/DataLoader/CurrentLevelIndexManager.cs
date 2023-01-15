using UnityEngine;

namespace _Homa.Sudoku.Scripts.DataLoader
{
    public class CurrentLevelIndexManager : MonoBehaviour
    {
        private const string CurrentLevelIndex = "CurrentLevelIndex";

        //This class should be remade with saving in the cloud or other types of saving to prevent easy cheating.
    
        public int LoadLevelIndex()
        {
            return PlayerPrefs.GetInt(CurrentLevelIndex, 0);
        }
    
        public void SaveLevelIndex(int levelIndex)
        {
            PlayerPrefs.SetInt(CurrentLevelIndex, levelIndex);
            PlayerPrefs.Save();   
        }
    
        private void OnApplicationQuit()
        {
        
        }
    }
}

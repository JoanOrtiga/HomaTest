using TMPro;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.SudokuStatus.CurrentLevelIndex
{
    public class CurrentLevelIndexView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentLevelIndexTxt;

        public void SetLevelIndex(int levelIndex)
        {
            currentLevelIndexTxt.text = (levelIndex + 1).ToString();
        }
    }
}

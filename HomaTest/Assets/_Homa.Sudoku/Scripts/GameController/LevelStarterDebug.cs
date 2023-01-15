using _Homa.Sudoku.Scripts.LevelData;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.GameController
{
    public class LevelStarterDebug : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private SudokuLevelData sudokuLevelData;

        private void Awake()
        {
            var sudokuManager = GetComponent<SudokuLevelManager>(); 
            
            sudokuManager.StartLevel(sudokuLevelData, 1);
        }
    }
}
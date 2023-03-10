using _Homa.Sudoku.Scripts.LevelData;
using _Homa.Sudoku.Scripts.SudokuStatus.CurrentLevelIndex;
using _Homa.Sudoku.Scripts.SudokuStatus.Mistakes;
using _Homa.Sudoku.Scripts.SudokuStatus.TimeStat;
using TMPro;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.GameController
{
    public class SudokuLevelManager : MonoBehaviour
    {
        [Header("Systems")] 
        [SerializeField] private SudokuTableManager sudokuTableManager;
        [SerializeField] private MistakeSystem mistakeSystem;
        [SerializeField] private SudokuProgress sudokuProgress;
        [SerializeField] private SudokuTime sudokuTime;
       
        [Header("References")]
        [SerializeField] private CurrentLevelIndexView currentLevelIndexView;

        [Header("Sudoku grid")]
        private SudokuLevelData _currentSudokuLevelData;

        public void StartLevel(SudokuLevelData sudokuLevelData, int levelIndex)
        {
            _currentSudokuLevelData = sudokuLevelData;
            
            _currentSudokuLevelData.CalculateMap();

            currentLevelIndexView.SetLevelIndex(levelIndex);
            
            sudokuProgress.SetInitialProgress(_currentSudokuLevelData.CellsWithNumberCount, _currentSudokuLevelData.TotalNumberOfCells);
            StartGame();
        }

        public void RestartLevel()
        {
            sudokuProgress.SetInitialProgress(_currentSudokuLevelData.CellsWithNumberCount, _currentSudokuLevelData.TotalNumberOfCells);
            StartGame();
        }

        private void StartGame()
        {
            sudokuTime.StartLevel();
            sudokuTableManager.SetupCells(_currentSudokuLevelData.TotalRows, _currentSudokuLevelData.TotalColumns, _currentSudokuLevelData.Map);  
            mistakeSystem.Setup(_currentSudokuLevelData.GetPossibleMistakes);
        }
    }
}

using _Homa.Library.Scripts.CommonUIElements.Percentage;
using _Homa.Sudoku.Scripts.Cell;
using _Homa.Sudoku.Scripts.SudokuStatus.Mistakes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Homa.Sudoku.Scripts.GameController
{
    public class SudokuProgress : MonoBehaviour
    {
        [SerializeField] private MistakeSystem mistakeSystem;
        [SerializeField] private PercentageViewUI percentageViewUI;
    
        private int _totalSudokuCells;
        private int _currentSudokuCells;

        public UnityEvent OnLevelCompleted;
        public UnityEvent OnLevelFailed;

        private void Awake()
        {
            mistakeSystem.OnLevelFailed += LevelFailed;
            CellController.OnGoodNumberOnCell += AddProgress;
        }

        private void AddProgress()
        {
            _currentSudokuCells++;
        
            percentageViewUI.SetPercentage(_currentSudokuCells, _totalSudokuCells);
        
            if(_currentSudokuCells >= _totalSudokuCells)
                LevelCompleted();
        }

        public void SetInitialProgress(int currentSudokuCells, int totalSudokuCells)
        {
            _currentSudokuCells = currentSudokuCells;
            _totalSudokuCells = totalSudokuCells;
        
            percentageViewUI.SetPercentage(_currentSudokuCells, _totalSudokuCells);
        }

        private void OnDestroy()
        {
            if(mistakeSystem != null)
                mistakeSystem.OnLevelFailed -= LevelFailed;
            CellController.OnGoodNumberOnCell -= AddProgress;
        }

        private void LevelCompleted()
        {
            OnLevelCompleted?.Invoke();
        }
    
        private void LevelFailed()
        {
            OnLevelFailed?.Invoke();
        }
    }
}

using System;
using _Homa.Library.Scripts.CommonUIElements.Percentage;
using _Homa.Sudoku.Scripts.Cell;
using _Homa.Sudoku.Scripts.SudokuStatus.Mistakes;
using _Homa.Sudoku.Scripts.SudokuStatus.TimeStat;
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
        
        [SerializeField] private LevelChangeAnimator levelChangeAnimator;
        [SerializeField] private SudokuTime sudokuTime;
    
        private int _totalSudokuCells;
        private int _currentSudokuCells;

        public UnityEvent OnLevelCompleted;
        public UnityEvent OnLevelFailed;

        private void Awake()
        {
            mistakeSystem.OnLevelFailed += LevelFailed;
            CellController.OnGoodNumberOnCell += AddProgress;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F))
            {
                LevelFailed();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                LevelCompleted();
            }
#endif
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
            levelChangeAnimator.LevelCompleteAnimation();
            sudokuTime.EndLevel();
        }
    
        private void LevelFailed()
        {
            levelChangeAnimator.LoseLevelAnimation();
            sudokuTime.EndLevel();
        }

        public void RaiseLevelFailed()
        {
            OnLevelFailed?.Invoke();
        }
        
        public void RaiseLevelCompleted()
        {
            OnLevelCompleted?.Invoke();
        }
    }
}

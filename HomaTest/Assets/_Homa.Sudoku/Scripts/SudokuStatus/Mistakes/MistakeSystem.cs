using System;
using _Homa.Sudoku.Scripts.Cell;
using UnityEngine;
using UnityEngine.Events;

namespace _Homa.Sudoku.Scripts.SudokuStatus.Mistakes
{
    public class MistakeSystem : MonoBehaviour
    {
        [SerializeField] private MistakesDisplayUI mistakesDisplayUI;

        private int _currentMistakes;
        private int _totalPossibleMistakes;
        public event Action OnLevelFailed;

        [SerializeField] private UnityEvent OnMadeMistake;

        private void Awake()
        {
            CellController.OnWrongNumberOnCell += MakeMistake;
        }

        public void Setup(int totalPossibleMistakes)
        {
            _totalPossibleMistakes = totalPossibleMistakes;
            mistakesDisplayUI.Setup(totalPossibleMistakes);
            _currentMistakes = 0;
        }

        private void MakeMistake()
        {
            _currentMistakes++;
            OnMadeMistake?.Invoke();
            mistakesDisplayUI.ShowMistake(_currentMistakes);

            if (_currentMistakes >= _totalPossibleMistakes)
            {
                OnLevelFailed?.Invoke();
            }
        }

        public void Reset()
        {
            _currentMistakes = 0;
            mistakesDisplayUI.ResetStatus();
        }

        private void OnDestroy()
        {
            CellController.OnWrongNumberOnCell -= MakeMistake;
        }
    }
}

using System;
using _Homa.Sudoku.Scripts.GameController;
using _Homa.Sudoku.Scripts.LevelData;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Cell
{
    public class CellController : MonoBehaviour
    {
        private ICellView _cellView;
        private CellClick _cellClick;

        private SudokuCell _sudokuCell;
        private SudokuTableAnimator _sudokuTableAnimator;

        public static event Action OnWrongNumberOnCell;
        public static event Action OnGoodNumberOnCell;
        
        public void Awake()
        {
            _cellView = GetComponent<ICellView>();
            _cellClick = GetComponent<CellClick>();
            _cellClick.OnCellClicked += CellGotClicked;
        }

        private void OnDestroy()
        {
            _cellClick.OnCellClicked -= CellGotClicked;
        }

        private void CellGotClicked()
        {
            if (SudokuClickAction.Instance.SelectedNumber == 0)
                return;
            if(IsInputCorrect(SudokuClickAction.Instance.SelectedNumber) && SudokuClickAction.Instance.SelectedNumber != 0)
            {
                _cellView.ShowData();
                _sudokuTableAnimator.StartAnimation(_sudokuCell.position);
                _cellClick.enabled = false;
                OnGoodNumberOnCell?.Invoke();
            }
            else
            {
                OnWrongNumberOnCell?.Invoke();
            }
        }

        public void FillData(SudokuCell sudokuCell, SudokuTableAnimator sudokuTableAnimator)
        {
            _sudokuTableAnimator = sudokuTableAnimator;
            _sudokuCell = sudokuCell;
            if (!_sudokuCell.data.inputByUser)
            {
                _cellClick.OnCellClicked -= CellGotClicked;
                Destroy(_cellClick);
            }
            _cellView.Initialize(_sudokuCell.data);
        }

        public bool IsInputCorrect(int numInput)
        {
            return numInput == _sudokuCell.data.numValue;
        }
    }
}

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

        private SudokuCellData _sudokuCellData;

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
            if(IsInputCorrect(SudokuClickAction.Instance.SelectedNumber) && SudokuClickAction.Instance.SelectedNumber != 0)
            {
                _cellView.ShowData();
                _cellClick.enabled = false;
                OnGoodNumberOnCell?.Invoke();
            }
            else
            {
                OnWrongNumberOnCell?.Invoke();
            }
        }

        public void FillData(SudokuCell sudokuCell)
        {
            _sudokuCellData = sudokuCell.data;
            if (!_sudokuCellData.inputByUser)
            {
                _cellClick.OnCellClicked -= CellGotClicked;
                Destroy(_cellClick);
            }
            _cellView.Initialize(_sudokuCellData);
        }

        public bool IsInputCorrect(int numInput)
        {
            return numInput == _sudokuCellData.numValue;
        }
    }
}

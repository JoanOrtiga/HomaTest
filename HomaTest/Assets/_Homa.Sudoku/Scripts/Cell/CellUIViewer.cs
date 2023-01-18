using _Homa.Sudoku.Scripts.LevelData;
using TMPro;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Cell
{
    public class CellUIViewer : MonoBehaviour , ICellView
    {
        [SerializeField] private TextMeshProUGUI number;
        [SerializeField] private Color colorIfInputByUser;

        private SudokuCellData _sudokuCellData;
        
        public void Initialize(SudokuCellData sudokuCellData)
        {
            _sudokuCellData = sudokuCellData;

            if (sudokuCellData.inputByUser)
            {
                number.color = colorIfInputByUser;
            }
            else
            {
                ShowData();
            }
        }

        public void ShowData()
        {
            number.text = _sudokuCellData.numValue.ToString();
        }
    }
}
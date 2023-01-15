using _Homa.Sudoku.Scripts.Cell;
using _Homa.Sudoku.Scripts.LevelData;
using UnityEngine;
using UnityEngine.UI;

namespace _Homa.Sudoku.Scripts.GameController
{
    public class SudokuTableManager2D : SudokuTableManager
    {
        [Header("Prefabs")]
        [SerializeField] private CellController _cellPrefab;

        [Header("References")] 
        [SerializeField] private Transform gridContainer;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        
        public override void SetupCells(int totalRows, int totalColumns, SudokuCell[,] map)
        {
            gridLayoutGroup.constraintCount = totalColumns;
            
            for (int row = 0; row < totalRows; row++)
            {
                for (int col = 0; col < totalColumns; col++)
                {
                    if (map[row, col] != null)
                    {
                        var newCell = Instantiate(_cellPrefab, gridContainer);
                        newCell.FillData(map[row,col]);
                    }
                    else
                    {
                        var emptyCell = new GameObject("EmptyCell", typeof(RectTransform));
                        emptyCell.transform.SetParent(gridContainer, false);
                    }
                }
            }
        }
    }
}

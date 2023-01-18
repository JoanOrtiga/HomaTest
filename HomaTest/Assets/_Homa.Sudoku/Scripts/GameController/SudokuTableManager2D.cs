using _Homa.Library.Scripts.DOTween;
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
        [SerializeField] private SudokuTableAnimator sudokuTableAnimator;

        public override void SetupCells(int totalRows, int totalColumns, SudokuCell[,] map)
        {
            ClearGrid();
            
            gridLayoutGroup.constraintCount = totalColumns;

            var scaleUIDOTween = new ScaleUI_DOTween[totalRows,totalColumns];
            
            for (int row = 0; row < totalRows; row++)
            {
                for (int col = 0; col < totalColumns; col++)
                {
                    if (map[row, col] != null)
                    {
                        var newCell = Instantiate(_cellPrefab, gridContainer);
                        newCell.FillData(map[row,col], sudokuTableAnimator);
                        scaleUIDOTween[row, col] = newCell.GetComponent<ScaleUI_DOTween>();
                    }
                    else
                    {
                        var emptyCell = new GameObject("EmptyCell", typeof(RectTransform));
                        emptyCell.transform.SetParent(gridContainer, false);
                    }
                }
            }
            
            sudokuTableAnimator.SetScaleUIMap(scaleUIDOTween);
        }

        private void ClearGrid()
        {
            sudokuTableAnimator.StopAnimations();
            
            for (int i = 0; i < gridContainer.childCount; i++)
            {
                Destroy(gridContainer.GetChild(i).gameObject);
            }
        }
    }
}

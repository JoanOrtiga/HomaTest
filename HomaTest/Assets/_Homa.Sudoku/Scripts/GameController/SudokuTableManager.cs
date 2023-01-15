using _Homa.Sudoku.Scripts.LevelData;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.GameController
{
    public abstract class SudokuTableManager : MonoBehaviour
    {
        public abstract void SetupCells(int totalWidth, int totalHeight, SudokuCell[,] sudokuCells);
    }
}
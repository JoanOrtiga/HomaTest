using System;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.LevelData
{
    [Serializable]
    public class SudokuCell
    {
        public SudokuCell(Vector2Int position)
        {
            this.position = position;
            data = new SudokuCellData();
        }
        
        public Vector2Int position;
        [SerializeField] public SudokuCellData data;
    }
}
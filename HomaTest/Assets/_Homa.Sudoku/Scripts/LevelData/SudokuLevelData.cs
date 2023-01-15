using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.LevelData
{
    [CreateAssetMenu(menuName = "Sudoku/Level", fileName = "LevelData")]
    public class SudokuLevelData : ScriptableObject
    {
        public static readonly string NameId = nameof(id);
        public static readonly string NamePossibleMistakes = nameof(possibleMistakes);
        public static readonly string NameCells = nameof(cells);
        public static readonly string NameTotalRows = nameof(totalRows);
        public static readonly string NameTotalColumns = nameof(totalColumns);

        [SerializeField] private string id;
        [SerializeField, Range(1, 7)] private int possibleMistakes = 3;
        [SerializeField] private List<SudokuCell> cells = new();
        [SerializeField] private int totalRows = 9;
        [SerializeField] public int totalColumns = 9;
        
        private SudokuCell[,] _map;
        public SudokuCell[,] Map
        {
            get
            {
                if (_map == null)
                {
                    CalculateMap();
                }
                return _map;
            }
        }

        public string Id
        {
            get => id;
            set => id = value;
        }
        
        public List<SudokuCell> Cells => cells;
        public int TotalRows => totalRows;
        public int TotalColumns => totalColumns;
        
        public int GetPossibleMistakes => possibleMistakes;
        public int CellsWithNumberCount { get; private set; } 
        public int EmptyCellsCount { get; private set; } 
        public int TotalNumberOfCells { get; private set; } 
        
        public void CalculateMap()
        {
            CalculateMap(this);
            CellsWithNumberCount = CalculateCellsWithNumberCount(cells);
            EmptyCellsCount = CalculateEmptyCellsCount(cells);
            TotalNumberOfCells = CalculateTotalNumberOfCells(cells);
        }

        public static void CalculateMap(SudokuLevelData sudokuLevelData)
        {
            sudokuLevelData._map = new SudokuCell[sudokuLevelData.totalRows, sudokuLevelData.totalColumns];
            for (int row = 0; row < sudokuLevelData._map.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuLevelData._map.GetLength(1); col++)
                {
                    sudokuLevelData._map[row, col] = sudokuLevelData.cells.FirstOrDefault(cell => cell.position.y == row && cell.position.x == col);
                }
            }
        }
        
        private static int CalculateCellsWithNumberCount(List<SudokuCell> cells) => cells.Count(cell => !cell.data.inputByUser);
        private static int CalculateEmptyCellsCount(List<SudokuCell> cells) => cells.Count(cell => cell.data.inputByUser);
        private static int CalculateTotalNumberOfCells(List<SudokuCell> cells) => cells.Count;
    }
}
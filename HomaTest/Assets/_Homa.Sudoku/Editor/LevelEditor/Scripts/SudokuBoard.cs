using System.Collections.Generic;
using System.Linq;
using _Homa.Sudoku.LevelEditor.Scripts;
using _Homa.Sudoku.Scripts.LevelData;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.Editor.LevelEditor.Scripts
{
    public class SudokuBoard
    {
        public static readonly Vector2 cellSize = new Vector2(30, 30);
        
        VisualElement _cornerCell = new VisualElement
        {
            style =
            {
                width = cellSize.x,
                height = cellSize.y
            }
        };

        private readonly VisualElement _board;
        private readonly ScrollView _sudokuBoardScrollView;
        
        private SudokuCellInspector _sudokuCellInspector;
        
        private readonly VisualTreeAsset _cell;
        private readonly VisualTreeAsset _rowItem;
        private readonly VisualTreeAsset _columnItem;

        private List<SudokuRowItem> _sudokuRowItems;
        private List<SudokuColumnItem> _sudokuColumnItems;
        private Dictionary<Vector2Int, SudokuCellItem> _sudokuCellItems;

        private SerializedProperty _sudokuCellsSerializedProperty;

        private SudokuCellItem _selectedSudokuCell;

        public SudokuBoard(VisualElement board, ScrollView sudokuBoardScrollView)
        {
            _board = board;
            _sudokuBoardScrollView = sudokuBoardScrollView;
            
            _sudokuCellItems = new Dictionary<Vector2Int, SudokuCellItem>();
            _sudokuRowItems = new List<SudokuRowItem>();
            _sudokuColumnItems = new List<SudokuColumnItem>();

            _sudokuCellInspector = new SudokuCellInspector(board.parent.parent, this);
            
            board.panel.visualTree.RegisterCallback<KeyDownEvent>(evt =>
            {
                if ((evt.modifiers & EventModifiers.Control) != 0 && evt.keyCode == KeyCode.Z ||
                    evt.keyCode == KeyCode.Y)
                {
                    var levelData = _sudokuCellsSerializedProperty.serializedObject.targetObject as SudokuLevelData;
                    BuildBoard(levelData.Cells, levelData.TotalRows, levelData.TotalColumns, _sudokuCellsSerializedProperty);
                }
            });
        }

        private void ClearBoard()
        {
            _board.Clear();
            
            _sudokuCellItems = new Dictionary<Vector2Int, SudokuCellItem>();
            _sudokuRowItems = new List<SudokuRowItem>();
            _sudokuColumnItems = new List<SudokuColumnItem>();
        }

        public void SelectCell(SudokuCellItem sudokuCellItem, SerializedProperty serializedProperty, int column, int row)
        {
            _selectedSudokuCell = sudokuCellItem;
            _sudokuCellInspector.SetCell(sudokuCellItem, serializedProperty, column, row);
        }

        public void RemoveCell()
        {
            var kvp = _sudokuCellItems.FirstOrDefault(kvp => kvp.Value == _selectedSudokuCell);
            _sudokuCellItems.Remove(kvp.Key);
            var levelData = _sudokuCellsSerializedProperty.serializedObject.targetObject as SudokuLevelData;
            var index = levelData.Cells.Remove(_selectedSudokuCell.GetSudokuCell);
          //  _sudokuCellsSerializedProperty.DeleteArrayElementAtIndex(index);
            _sudokuCellsSerializedProperty.serializedObject.ApplyModifiedProperties();
            
            BuildBoard(levelData.Cells, levelData.TotalRows, levelData.TotalColumns, _sudokuCellsSerializedProperty);
        }

        public void AddCell(SudokuCellItem sudokuCellItem)
        {
            foreach (var dSudokuCellItem in _sudokuCellItems)
            {
                if (dSudokuCellItem.Value == sudokuCellItem)
                {
                    _sudokuCellsSerializedProperty.InsertArrayElementAtIndex(_sudokuCellsSerializedProperty.arraySize);
                    _sudokuCellsSerializedProperty.serializedObject.ApplyModifiedProperties();

                    var sudokuLevelData = _sudokuCellsSerializedProperty.serializedObject.targetObject as SudokuLevelData;
                    var newSudokuCell = sudokuLevelData?.Cells[_sudokuCellsSerializedProperty.arraySize-1];
                    newSudokuCell.position = dSudokuCellItem.Key;
                    
                    _sudokuCellsSerializedProperty.serializedObject.ApplyModifiedProperties();


                    sudokuCellItem.SetData(newSudokuCell, _sudokuCellsSerializedProperty.GetArrayElementAtIndex(_sudokuCellsSerializedProperty.arraySize-1));
                }
            }
        }
        
        public void BuildBoard(List<SudokuCell> cells, int rows, int columns, SerializedProperty sudokuCellsSerializedProperty)
        {
            _sudokuCellsSerializedProperty = sudokuCellsSerializedProperty;
            
            ClearBoard();
            _board.style.width = (columns + 1) * cellSize.x;
            _board.style.height = (rows + 1) * cellSize.y;

            _board.Add(_cornerCell);
            
            List<SudokuCell> unusedCells = new List<SudokuCell>(cells);

            for (int i = 0; i < columns; i++)
            {
                var sudokuColumnItem = SudokuColumnItem.Spawn(_board);
                _sudokuColumnItems.Add(sudokuColumnItem);
                sudokuColumnItem.SetNumber(i+1);
            }

            for (int i = 0; i < rows; i++)
            {
                var sudokuRowItem = SudokuRowItem.Spawn(_board);
                _sudokuRowItems.Add(sudokuRowItem);
                sudokuRowItem.SetNumber(i+1);

                for (int j = 0; j < columns; j++)
                {
                    var sudokuCellItem = SudokuCellItem.Spawn(_board);
                    _sudokuCellItems.Add(new Vector2Int(j, i), sudokuCellItem);

                    var cell = unusedCells.FirstOrDefault(cell => cell.position.x == j && cell.position.y == i);
                    SerializedProperty serializedCell = null;
                    if (cell != null)
                    {
                        unusedCells.Remove(cell);
                        serializedCell = sudokuCellsSerializedProperty.GetArrayElementAtIndex(cells.IndexOf(cell));
                    }

                    sudokuCellItem.Init(cell, serializedCell, this);
                }
            }
        }

        public void CheckForErrors()
        {
            string errorMessage = string.Empty;
            
            for (int i = 0; i < _sudokuRowItems.Count; i++)
            {
                var rowCells = _sudokuCellItems.Where(sudokuCellItem => sudokuCellItem.Key.y == i).ToList();
                
                for (int j = 0; j < rowCells.Count; j++)
                {
                    if(rowCells[j].Value.GetSudokuCell == null)
                        continue;
                    var num = rowCells[j].Value.GetSudokuCell.data.numValue;
                    var wrongCells = rowCells.Where(cell => num == cell.Value.GetSudokuCell?.data.numValue).ToList();
                    wrongCells.RemoveAll(cell => cell.Key == rowCells[j].Key);
                    foreach (var wrongCell in wrongCells)
                    {
                        rowCells.RemoveAll(cell => cell.Key == wrongCell.Key);
                    }
                    
                    if(wrongCells.Count == 0)
                        continue;

                    errorMessage = $"Duplicated number \"{num}\" in row {j}. Cells:";
                    errorMessage += $" {new Vector2Int(rowCells[j].Key.x+1, rowCells[j].Key.y+1)}";
                    foreach (var wrongCell in wrongCells)
                    {
                        errorMessage += $" {new Vector2Int(wrongCell.Key.x+1, wrongCell.Key.y+1)}";
                    }
                    errorMessage += ".\n";

                }
            }
            
            for (int i = 0; i < _sudokuColumnItems.Count; i++)
            {
                var colCells = _sudokuCellItems.Where(sudokuCellItem => sudokuCellItem.Key.x == i).ToList();

                for (int j = 0; j < colCells.Count; j++)
                {
                    if(colCells[j].Value.GetSudokuCell == null)
                        continue;
                    var num = colCells[j].Value.GetSudokuCell.data.numValue;
                    var wrongCells = colCells.Where(cell => num == cell.Value.GetSudokuCell?.data.numValue).ToList();
                    wrongCells.RemoveAll(cell => cell.Key == colCells[j].Key);
                    foreach (var wrongCell in wrongCells)
                    {
                        colCells.RemoveAll(cell => cell.Key == wrongCell.Key);
                    }
                    
                    if(wrongCells.Count == 0)
                        continue;

                    errorMessage = $"Duplicated number \"{num}\" in col {j}. Cells:";
                    errorMessage += $" {new Vector2Int(colCells[j].Key.x+1, colCells[j].Key.y+1)}";
                    foreach (var wrongCell in wrongCells)
                    {
                        errorMessage += $" {new Vector2Int(wrongCell.Key.x+1, wrongCell.Key.y+1)}";
                    }
                    errorMessage += ".\n";

                }
            }

            SudokuEditorConsole.Instance.PrintConsole(errorMessage);
        }
    }
}
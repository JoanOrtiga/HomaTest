using _Homa.Library.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.Editor.LevelEditor.Scripts
{
    public class SudokuCellInspector
    {
        private static class SudokuCellInspectorUxml
        {
            public const string SudokuCellInspector = "SudokuCellInspector";
            public const string RowField = "RowPosition";
            public const string ColumnField = "ColumnPosition";
            public const string RemoveCellButton = "RemoveCellButton";
        }
        
        private VisualElement _sudokuCellInspectorVisualElement;

        private IntegerField _rowField;
        private IntegerField _columnField;
        private Button _removeCellButton;

        private SudokuBoard _sudokuBoard;

        private static SudokuCellInspector _sudokuCellInspector;

        private SudokuCellItem _currentSudokuCellItem;
        
        public SudokuCellInspector(VisualElement parent, SudokuBoard sudokuBoard)
        {
            _sudokuCellInspectorVisualElement = parent.Q<VisualElement>(SudokuCellInspectorUxml.SudokuCellInspector);
            
            _sudokuBoard = sudokuBoard;

            _rowField = parent.Q<IntegerField>(SudokuCellInspectorUxml.RowField);
            _columnField = parent.Q<IntegerField>(SudokuCellInspectorUxml.ColumnField);
            _removeCellButton = parent.Q<Button>(SudokuCellInspectorUxml.RemoveCellButton);
            _removeCellButton.clicked += RemoveCell;
            _sudokuCellInspector = this;
        }

        private void RemoveCell()
        {
            _sudokuBoard.RemoveCell();
        }

        ~SudokuCellInspector()
        {
            _removeCellButton.clicked -= RemoveCell;
        }

        public void SetCell(SudokuCellItem sudokuCellItem, SerializedProperty serializedProperty, int column, int row)
        {
            _currentSudokuCellItem = sudokuCellItem;
            
            _rowField.value = row;
            _columnField.value = column;
            
            _sudokuCellInspectorVisualElement.Clear();
            
            var serializedProperties = serializedProperty.GetDirectChildren();
            foreach (var sp in serializedProperties)
            {
                var pf = new PropertyField(sp);
                pf.Bind(serializedProperty.serializedObject);
                _sudokuCellInspectorVisualElement.Add(pf);

                if (sp.name == "numValue")
                {
                    pf.RegisterValueChangeCallback(evt =>
                    {
                        _sudokuBoard.CheckForErrors();
                    });
                }

                if (sp.name == "inputByUser")
                {
                    pf.RegisterValueChangeCallback(evt =>
                    {
                        _currentSudokuCellItem.UpdateColor(evt.changedProperty.boolValue);
                    });
                }
            }
        }
    }
}
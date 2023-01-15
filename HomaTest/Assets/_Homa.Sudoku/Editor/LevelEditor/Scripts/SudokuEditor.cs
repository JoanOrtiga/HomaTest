using _Homa.Sudoku.Editor.LevelEditor.Scripts;
using _Homa.Sudoku.Scripts.Editor.SudokuLevelEditor;
using _Homa.Sudoku.Scripts.LevelData;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.LevelEditor.Scripts
{
    public class SudokuEditor
    {
        private static class SudokuEditorUXML
        {
            public const string NoLevelSelected = "NoLevelSelected";
            public const string LevelSelected = "LevelSelected";
            
            public const string NameField = "NameField";
            public const string RowsField = "RowsField";
            public const string ColumnsField = "ColumnsField";
            public const string MistakesField = "MistakesField";

            public const string SudokuBoard = "SudokuBoard";
            public const string SudokuBoardScrollView = "SudokuBoardScrollView";
        }
        
        private readonly VisualElement _visualElement;
        private readonly SudokuLevelEditor _sudokuLevelEditor;

        private VisualElement _levelSelected;
        private VisualElement _noLevelSelected;
        
        private TextField _nameField;
        private SliderInt _mistakesField;
        private IntegerField _rowsField;
        private IntegerField _columnsField;

        private SudokuLevelData _currentSudokuLevelData;
        private SerializedObject _sudokuLevelDataSerialized;
        private SudokuBoard _sudokuBoard;
        private SudokuEditorConsole _sudokuEditorConsole;
        
        public SudokuEditor(VisualElement sudokuEditor, SudokuLevelEditor sudokuLevelEditor)
        {
            _visualElement = sudokuEditor;
            _sudokuLevelEditor = sudokuLevelEditor;

            _sudokuEditorConsole = new SudokuEditorConsole(sudokuEditor);
            
            QueryElements();
            SetItemSelected(false);
            DisplayItem(ScriptableObject.CreateInstance<SudokuLevelData>());
        }

        private void QueryElements()
        {
            _levelSelected = _visualElement.Q(SudokuEditorUXML.LevelSelected);
            _noLevelSelected = _visualElement.Q(SudokuEditorUXML.NoLevelSelected);
            
            _nameField = _visualElement.Q<TextField>(SudokuEditorUXML.NameField);
            _nameField.bindingPath = SudokuLevelData.NameId;
            _nameField.RegisterValueChangedCallback(evt => 
            {
                _currentSudokuLevelData.Id = evt.newValue;
                _currentSudokuLevelData.name = evt.newValue;
                _sudokuLevelEditor.UpdateItem(_currentSudokuLevelData);
            });

            _mistakesField = _visualElement.Q<SliderInt>(SudokuEditorUXML.MistakesField);
            _mistakesField.bindingPath = SudokuLevelData.NamePossibleMistakes;
            
            _rowsField = _visualElement.Q<IntegerField>(SudokuEditorUXML.RowsField);
            _rowsField.bindingPath = SudokuLevelData.NameTotalRows;
            _rowsField.RegisterValueChangedCallback(evt =>
            {
                _sudokuBoard.BuildBoard(_currentSudokuLevelData.Cells, _currentSudokuLevelData.TotalRows, _currentSudokuLevelData.TotalColumns, _sudokuLevelDataSerialized.FindProperty(SudokuLevelData.NameCells));
            });

            _columnsField = _visualElement.Q<IntegerField>(SudokuEditorUXML.ColumnsField);
            _columnsField.bindingPath = SudokuLevelData.NameTotalColumns;
            _columnsField.RegisterValueChangedCallback(evt =>
            {
                _sudokuBoard.BuildBoard(_currentSudokuLevelData.Cells, _currentSudokuLevelData.TotalRows, _currentSudokuLevelData.TotalColumns, _sudokuLevelDataSerialized.FindProperty(SudokuLevelData.NameCells));
            });

            var sudokuBoard = _visualElement.Q<VisualElement>(SudokuEditorUXML.SudokuBoard);
            var sudokuBoardScrollView = _visualElement.Q<ScrollView>(SudokuEditorUXML.SudokuBoardScrollView);
            _sudokuBoard = new SudokuBoard(sudokuBoard, sudokuBoardScrollView);
        }
        
        private void SetItemSelected(bool isItemSelected)
        {
            _levelSelected.style.display = isItemSelected ? DisplayStyle.Flex : DisplayStyle.None;
            _noLevelSelected.style.display = isItemSelected ? DisplayStyle.None : DisplayStyle.Flex;
        }
        
        public void DisplayItem(SudokuLevelData sudokuLevelData)
        {
            _currentSudokuLevelData = sudokuLevelData;
            
            if (sudokuLevelData == null)
            {
                SetItemSelected(false);
                return;
            }
            
            _sudokuLevelDataSerialized = new SerializedObject(sudokuLevelData);
            _levelSelected.Bind(_sudokuLevelDataSerialized);
            SetItemSelected(true);
            
            _sudokuBoard.BuildBoard(sudokuLevelData.Cells, sudokuLevelData.TotalRows, sudokuLevelData.totalColumns, _sudokuLevelDataSerialized.FindProperty(SudokuLevelData.NameCells));
        }
    }
}
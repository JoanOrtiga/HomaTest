using _Homa.Sudoku.Scripts.LevelData;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.Editor.LevelEditor.Scripts
{
    public class SudokuCellItem
    {
        private static class SudokuCellItemUxml
        {
            private const string SudokuCellUxmlPath = "SudokuCell";
            public const string Cell = "Cell";
            public const string SelectButton = "SelectButton";
            public const string UnusedCell = "UnusedCell";
            public const string AddCellButton = "AddCell";
            
            private static VisualTreeAsset _cellItemVisualTreeAsset;
            public static VisualTreeAsset GetVisualTreeAsset()
            {
                if (_cellItemVisualTreeAsset == null)
                {
                    _cellItemVisualTreeAsset = Resources.Load<VisualTreeAsset>(SudokuCellUxmlPath);
                }

                return _cellItemVisualTreeAsset;
            }

            public static readonly Color inputByUserColor = new Color(0.2268067f, 0.4339623f, 0.3044095f);
            public static readonly Color defaultColor = new Color(0.3764706f, 0.3764706f, 0.3764706f);
        }

        public static SudokuCellItem Spawn(VisualElement parent)
        {
            var vta = SudokuCellItemUxml.GetVisualTreeAsset();
            var cellItemVisualElement = vta.Instantiate();
            parent.Add(cellItemVisualElement);
            return new SudokuCellItem(cellItemVisualElement);
        }

        private readonly VisualElement _cellItem;
        private readonly Button _addCellButton;
        private readonly VisualElement _unusedCell;
        private readonly Button _selectCellButton;
        private readonly VisualElement _selectedCell;
        
        private SudokuCell _sudokuCell;
        private SerializedProperty _sudokuCellSerializedProperty;
        private SudokuBoard _sudokuBoard;

        public SudokuCell GetSudokuCell => _sudokuCell;
        
        private SudokuCellItem(VisualElement cellItemVisualElement)
        {
            _cellItem = cellItemVisualElement;

            _addCellButton = _cellItem.Q<Button>(SudokuCellItemUxml.AddCellButton);
            _addCellButton.clicked += NewData;
            _unusedCell = _cellItem.Q<VisualElement>(SudokuCellItemUxml.UnusedCell);
            _selectCellButton = _cellItem.Q<Button>(SudokuCellItemUxml.SelectButton);
            _selectCellButton.clicked += SelectCell;
            _selectedCell = _cellItem.Q<VisualElement>(SudokuCellItemUxml.Cell);

        }
        
        ~SudokuCellItem()
        {
            _selectCellButton.clicked -= SelectCell;
            _selectCellButton.clicked -= NewData;
        }

        public void Init(SudokuCell sudokuCell, SerializedProperty sudokuCellSerializedProperty, SudokuBoard sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
            SetData(sudokuCell, sudokuCellSerializedProperty);
        }
        
        public void SetData(SudokuCell sudokuCell, SerializedProperty sudokuCellSerializedProperty)
        {
            _sudokuCell = sudokuCell;
            
            if (_sudokuCell == null)
            {
                _unusedCell.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                _selectedCell.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
                return;
            }
            
            _unusedCell.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _selectedCell.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

            _sudokuCellSerializedProperty = sudokuCellSerializedProperty;
            var cellData = sudokuCellSerializedProperty.FindPropertyRelative(nameof(SudokuCell.data));
            var numValueProperty = cellData.FindPropertyRelative(nameof(SudokuCellData.numValue));
            _selectCellButton.BindProperty(numValueProperty);
            
            UpdateColor(_sudokuCell.data.inputByUser);
            
            SelectCell();
        }

        public void UpdateColor(bool inputByUser)
        {
            _selectCellButton.style.backgroundColor =
                inputByUser ? SudokuCellItemUxml.inputByUserColor : SudokuCellItemUxml.defaultColor;
        }
        
        private void SelectCell()
        {
            _sudokuBoard.SelectCell(this, _sudokuCellSerializedProperty.FindPropertyRelative(nameof(SudokuCell.data)),_sudokuCell.position.x + 1, _sudokuCell.position.y + 1);
        }
        
        private void NewData()
        {
            _sudokuBoard.AddCell(this);
        }
    }
}
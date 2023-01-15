using UnityEngine;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.Editor.LevelEditor.Scripts
{
    public class SudokuColumnItem
    {
        private static class SudokuColumnItemUxml
        {
            private const string SudokuColumnItemUxmlPath = "SudokuColumnItem";
            public const string ColumnItem = "ColumnItem";
            public const string ColumnNumber = "ColumnNumber";
            
            private static VisualTreeAsset _columnItemVisualTreeAsset;
            public static VisualTreeAsset GetVisualTreeAsset()
            {
                if (_columnItemVisualTreeAsset == null)
                {
                    _columnItemVisualTreeAsset = Resources.Load<VisualTreeAsset>(SudokuColumnItemUxmlPath);
                }

                return _columnItemVisualTreeAsset;
            }
        }

        public static SudokuColumnItem Spawn(VisualElement parent)
        {
            var vta = SudokuColumnItemUxml.GetVisualTreeAsset();
            var columnItemVisualElement = vta.Instantiate();
            parent.Add(columnItemVisualElement);
            return new SudokuColumnItem(columnItemVisualElement);
        }

        private VisualElement _columnItem;
        private Label _columNumberTxt;

        private SudokuColumnItem(VisualElement columnItemVisualElement)
        {
            _columnItem = columnItemVisualElement;

            _columNumberTxt = _columnItem.Q<Label>(SudokuColumnItemUxml.ColumnNumber);
        }

        public void SetNumber(int number)
        {
            _columNumberTxt.text = number.ToString();
        }
    }
}
using UnityEngine;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.Editor.LevelEditor.Scripts
{
    public class SudokuRowItem
    {
        private static class SudokuRowItemUxml
        {
            private const string SudokuRowItemUxmlPath = "SudokuRowItem";
            public const string RowItem = "RowItem";
            public const string RowNumber = "RowNumber";
            
            private static VisualTreeAsset _rowItemVisualTreeAsset;
            public static VisualTreeAsset GetVisualTreeAsset()
            {
                if (_rowItemVisualTreeAsset == null)
                {
                    _rowItemVisualTreeAsset = Resources.Load<VisualTreeAsset>(SudokuRowItemUxmlPath);
                }

                return _rowItemVisualTreeAsset;
            }
        }
        
        public static SudokuRowItem Spawn(VisualElement parent)
        {
            var vta = SudokuRowItemUxml.GetVisualTreeAsset();
            var rowItemVisualElement = vta.Instantiate();
            parent.Add(rowItemVisualElement);
            return new SudokuRowItem(rowItemVisualElement);
        }

        private VisualElement _rowItem;
        private Label _rowNumberTxt;

        private SudokuRowItem(VisualElement rowItemVisualElement)
        {
            _rowItem = rowItemVisualElement;
            
            _rowNumberTxt = _rowItem.Q<Label>(SudokuRowItemUxml.RowNumber);
        }
        
        public void SetNumber(int number)
        {
            _rowNumberTxt.text = number.ToString();
        }
    }
}
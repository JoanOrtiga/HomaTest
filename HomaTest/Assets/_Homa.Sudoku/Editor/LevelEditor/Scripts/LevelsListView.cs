using System;
using System.Collections.Generic;
using System.Linq;
using _Homa.Sudoku.Scripts.LevelData;
using NUnit.Framework;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.Editor.LevelEditor.Scripts
{
    public class LevelsListView : ListView
    {
        public new class UxmlFactory : UxmlFactory<LevelsListView, ListView.UxmlTraits> { }

        private const string UnnamedLevel = "Level";

        private string _currentFilter = string.Empty;
        
        private ToolbarSearchField _searchField;
        
        private List<SudokuLevelData> _list = null;
        private List<SudokuLevelData> _filteredList = null;

        public event Action<SudokuLevelData> OnLevelSelected;
        public event Action<SudokuLevelData> OnCtxMenuCloneLevel; 
        public event Action<SudokuLevelData> OnCtxMenuDeleteLevel; 
        
        public LevelsListView()
        {
            makeItem = CreateVisualItem;
            bindItem = BindItem;
            itemsSource = _list;

          //  ItemModuleEditorSettings.instance.OnDoubleClickToSelectChanged += ChangeOnElementSelectedBehaviour;
            ChangeOnElementSelectedBehaviour(false);
        }

        public void Init(ToolbarSearchField searchField)
        {
            _searchField = searchField;
            _searchField.RegisterValueChangedCallback(evt =>
            {
                if (_list == null)
                {
                    CleanFilter();
                    return;
                }
                _currentFilter = evt.newValue.ToLower();
                Filter();
            });
        }
        
        private VisualElement CreateVisualItem()
        {
            return new Label();
        }
        
        private void BindItem(VisualElement visualElement, int index)
        {
            var label = visualElement as Label;
            visualElement.AddManipulator(new ContextualMenuManipulator(evt =>
            {
                evt.menu.AppendAction("Clone", evt =>
                {
                    OnCtxMenuCloneLevel?.Invoke(_filteredList[index]);
                });
                evt.menu.AppendAction("Delete", evt =>
                {
                    OnCtxMenuDeleteLevel?.Invoke(_filteredList[index]);
                });
            }));
            var fill = _filteredList[index].Id;
            if (string.IsNullOrEmpty(fill))
            {
                _filteredList[index].Id = $"{UnnamedLevel} {index}";
            }

            if (string.IsNullOrEmpty(_filteredList[index].name))
            {
                _filteredList[index].name = $"{UnnamedLevel} {index}";
            }
            
            label.text = _filteredList[index].Id;
        }
        
        public void UpdateList(List<SudokuLevelData> updateList)
        {
            _list = updateList;
            CleanFilter();
            if (updateList == null)
            {
                itemsSource = new List<string>();
                Rebuild();
                return;
            }
            
            Filter();
            itemsSource = _filteredList;
            Rebuild();
            if(itemsSource.Count != 0)
                SetSelection(0);
        }
        
        private void CleanFilter()
        {
            _searchField.SetValueWithoutNotify(string.Empty);
            _currentFilter = string.Empty;
        }
        
        private void Filter()
        {
            _filteredList = _list;
            if (_currentFilter == string.Empty)
            {
                itemsSource = _filteredList;
                Rebuild();
                return;
            }
            
            _filteredList = _filteredList.Where(sudokuLevelData => sudokuLevelData.Id != null && sudokuLevelData.Id.ToLower().Contains(_currentFilter)).ToList();
            itemsSource = _filteredList;
            Rebuild();
            RefreshItems();
        }

        public void AddElement(SudokuLevelData sudokuLevelData)
        {
            _list.Add(sudokuLevelData);
            SetSelection(_list.IndexOf(sudokuLevelData));
            Filter();  
        }
        
        private void OnElementSelected(IEnumerable<object> element)
        {
            var list = element.ToList();
            if (list.Count != 0)
            {   
                OnLevelSelected?.Invoke(list[0] as SudokuLevelData);
            }
        }
        
        private void ChangeOnElementSelectedBehaviour(bool doubleClickToSelect)
        {
            if (doubleClickToSelect)
            {
                onItemsChosen += OnElementSelected;
                onSelectionChange -= OnElementSelected;
            }
            else
            {
                onItemsChosen -= OnElementSelected;
                onSelectionChange += OnElementSelected;
            }
        }
        
        public void UpdateItem(SudokuLevelData sudokuLevelData)
        {
            if (sudokuLevelData == null)
                return;
            RefreshItem(_filteredList.IndexOf(sudokuLevelData));
            Filter();
        }

        public void RemoveElement(SudokuLevelData currentSudokuLevelData)
        {
            int index = _list.IndexOf(currentSudokuLevelData);
            int filteredListIndex = _filteredList.IndexOf(currentSudokuLevelData);
            _list.RemoveAt(index);
            Filter();
            if (_filteredList.Count > 0)
            {
                if (filteredListIndex == _filteredList.Count)
                    SetSelection(filteredListIndex - 1);
                else
                    SetSelection(filteredListIndex);
            }
            else
                OnLevelSelected?.Invoke(null);
        }
    }
}

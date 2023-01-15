using System;
using _Homa.Library.Editor;
using _Homa.Sudoku.LevelEditor.Scripts;
using _Homa.Sudoku.Scripts.LevelData;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.Editor.LevelEditor.Scripts
{
    public class SudokuLevelSelector
    {
        private readonly VisualElement _visualElement;
        private readonly SudokuEditor _sudokuEditor;

        private ObjectField _levelsCollection;
        private Button _addLevelButton;
        private Button _removeLevelButton;
        private ToolbarSearchField _searchBar;
        private LevelsListView _levelsListView;

        public event Action<SudokuLevelsCollection> OnCollectionChanged;

        private SudokuLevelsCollection _currentSudokuLevelsCollection;
        public bool HasCollection => _currentSudokuLevelsCollection != null;

        private SudokuLevelData _currentSudokuLevelData;
        
        public SudokuLevelSelector(VisualElement sudokuLevelEditor, SudokuEditor sudokuEditor)
        {
            _visualElement = sudokuLevelEditor;
            _sudokuEditor = sudokuEditor;
            
            OnLevelSelected(null);
            OnCollectionChanged += CollectionUpdated;
            QueryElements();
        }

        ~SudokuLevelSelector()
        {
            OnCollectionChanged -= CollectionUpdated;
            _addLevelButton.clicked -= AddNewLevel;
            _removeLevelButton.clicked -= RemoveLevel;
        }

        private void QueryElements() 
        {
            _levelsCollection = _visualElement.Q<ObjectField>(SudokuLevelSelectorUxml.LevelsCollection);
            _levelsCollection.objectType = typeof(SudokuLevelsCollection);
            _levelsCollection.RegisterValueChangedCallback(evt =>
            {
                OnCollectionChanged?.Invoke(evt.newValue as SudokuLevelsCollection);
            });

            _addLevelButton = _visualElement.Q<Button>(SudokuLevelSelectorUxml.AddLevelButton);
            _addLevelButton.clicked += AddNewLevel;
            _removeLevelButton = _visualElement.Q<Button>(SudokuLevelSelectorUxml.RemoveLevelButton);
            _removeLevelButton.clicked += RemoveLevel;

            _searchBar = _visualElement.Q<ToolbarSearchField>(SudokuLevelSelectorUxml.SearchBar);
            
            _levelsListView = _visualElement.Q<LevelsListView>(SudokuLevelSelectorUxml.LevelsList);
            _levelsListView.Init(_searchBar);
            _levelsListView.OnLevelSelected += OnLevelSelected;
            _levelsListView.OnCtxMenuCloneLevel += CloneLevel;
            _levelsListView.OnCtxMenuDeleteLevel += RemoveLevel;
        }
        
        private void CloneLevel(SudokuLevelData sudokuLevelData)
        {
            AddNewLevel(sudokuLevelData.Clone());
        }

        private void RemoveLevel(SudokuLevelData sudokuLevelData)
        {
            AssetDatabase.RemoveObjectFromAsset(sudokuLevelData);
            _levelsListView.RemoveElement(sudokuLevelData);
            AssetDatabase.SaveAssets();        
        }
        
        private void RemoveLevel()
        {
            if (!HasCollection || _currentSudokuLevelData == null)
                return;
            
            RemoveLevel(_currentSudokuLevelData);
        }
        
        private void AddNewLevel()
        {
            if (!HasCollection)
                return;
            
            var newSudokuLevelData = ScriptableObject.CreateInstance<SudokuLevelData>();
            AddNewLevel(newSudokuLevelData);
        }
        
        private void AddNewLevel(SudokuLevelData sudokuLevelData) {
            AssetDatabase.AddObjectToAsset(sudokuLevelData, _currentSudokuLevelsCollection);
            AssetDatabase.SaveAssets();
            _levelsListView.AddElement(sudokuLevelData); 
        }
        
        private void OnLevelSelected(SudokuLevelData sudokuLevelData)
        {
            _currentSudokuLevelData = sudokuLevelData;
            _sudokuEditor.DisplayItem(sudokuLevelData);
        }
        
        private void CollectionUpdated(SudokuLevelsCollection newSudokuLevelsCollection)
        {
            _currentSudokuLevelsCollection = newSudokuLevelsCollection;

            if (!HasCollection)
            {
                OnLevelSelected(null);
                _levelsListView.UpdateList(null);
                return;
            }
            
            OnLevelSelected(null);
            _levelsListView.UpdateList(null);
            _levelsListView.UpdateList(newSudokuLevelsCollection.Levels);
        }
        
        public void UpdateLevel(SudokuLevelData sudokuLevelData)
        {
            _levelsListView.UpdateItem(sudokuLevelData);
        }
        
        public void SetLevelsCollection(SudokuLevelsCollection sudokuLevelsCollection)
        {
            _levelsCollection.value = sudokuLevelsCollection;
        }

        private static class SudokuLevelSelectorUxml
        {
            public const string LevelsCollection = "LevelsCollection";
            public const string LevelsList = "LevelsList";
            public const string AddLevelButton = "AddLevelButton";
            public const string RemoveLevelButton = "RemoveLevelButton";
            public const string SearchBar = "SearchBar";
        }


    }
}
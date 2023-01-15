using System;
using _Homa.Sudoku.Editor.LevelEditor.Scripts;
using _Homa.Sudoku.LevelEditor.Scripts;
using _Homa.Sudoku.Scripts.LevelData;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.Scripts.Editor.SudokuLevelEditor
{
    public class SudokuLevelEditor : EditorWindow
    {
        private SudokuEditor _sudokuEditor;
        private SudokuLevelSelector _sudokuLevelSelector;
        

        [MenuItem("Tools/Sudoku/Level Editor")]
        public static void OpenSudokuLevelEditor()
        {
            NewWindow();
        }
        
        [MenuItem("Tools/Sudoku/Level Editor Close")]
        public static void Close()
        {
            SudokuLevelEditor wnd = GetWindow<SudokuLevelEditor>();
            ((EditorWindow)wnd).Close();
        }

        private static SudokuLevelEditor NewWindow()
        {
            SudokuLevelEditor wnd = GetWindow<SudokuLevelEditor>();
            wnd.titleContent = new GUIContent("Sudoku level editor");
            wnd.minSize = new Vector2(800, 500);
            return wnd;
        }

        public static void OpenItemModule(SudokuLevelsCollection sudokuLevelsCollection)
        {
            var wnd = NewWindow();
            wnd.SetLevelCollection(sudokuLevelsCollection);
        }

        private void SetLevelCollection(SudokuLevelsCollection sudokuLevelsCollection)
        {
            _sudokuLevelSelector.SetLevelsCollection(sudokuLevelsCollection);
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            var visualTree = Resources.Load<VisualTreeAsset>(SudokuLevelEditorUxml.UxmlPath);
            visualTree.CloneTree(root);

            PopulateWindow(root);
        }

        private void PopulateWindow(VisualElement root)
        {
            var sudokuEditor = root.Q<VisualElement>(SudokuLevelEditorUxml.SudokuEditor);
            Assert.IsNotNull(sudokuEditor, "sudokuEditor != null");
            _sudokuEditor = new SudokuEditor(sudokuEditor, this);

            var levelSelector = root.Q<VisualElement>(SudokuLevelEditorUxml.LevelSelector);
            Assert.IsNotNull(levelSelector, "levelSelector != null");
            _sudokuLevelSelector = new SudokuLevelSelector(levelSelector, _sudokuEditor);
        }

        public void UpdateItem(SudokuLevelData currentSudokuLevelData)
        {
            _sudokuLevelSelector?.UpdateLevel(currentSudokuLevelData);
        }
    }

    public static class SudokuLevelEditorUxml
    {
        //In resources folder:
        public const string UxmlPath = "SudokuLevelEditor";
        
        //UXML Elements name:
        public const string SudokuEditor = "SudokuEditor";
        public const string LevelSelector = "LevelSelector";
    }
}

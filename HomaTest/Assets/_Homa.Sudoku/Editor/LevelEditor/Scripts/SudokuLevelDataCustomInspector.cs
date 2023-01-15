using _Homa.Sudoku.Scripts.LevelData;
using UnityEditor;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Editor.SudokuLevelEditor
{
    [CustomEditor(typeof(SudokuLevelData))]
    public class SudokuLevelDataCustomInspector : UnityEditor.Editor
    {
        
        //TODO Custom Inspector Sudoku Level.
        /*
        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our inspector UI
            VisualElement myInspector = new VisualElement();

            // Add a simple label
            myInspector.Add(new Label("This is a custom inspector"));

            // Return the finished inspector UI
            return myInspector;
        }*/

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var sudokuLevelData = target as SudokuLevelData;
            
            if (GUILayout.Button("Generate map"))
            {
                sudokuLevelData.CalculateMap();
            }
        }
    }
}
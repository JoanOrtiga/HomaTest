using UnityEditor;
using UnityEngine.UIElements;

namespace _HomaSudoku.Scripts.Editor
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
    }
}
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace _Homa.Sudoku.LevelEditor.Scripts
{
    public class SudokuEditorConsole
    {
        private class SudokuEditorConsoleUxml
        {
            public const string ConsoleText = "ConsoleText";
        }

        private const string NoErrors = "No errors.";


        private Label _consoleText;
        public static SudokuEditorConsole Instance { get; private set; }


        public SudokuEditorConsole(VisualElement parent)
        {
            _consoleText = parent.Q<Label>(SudokuEditorConsoleUxml.ConsoleText);
            Instance = this;
            ResetConsole();
        }

        private void ResetConsole()
        {
            _consoleText.text = NoErrors;
        }

        public void PrintConsole(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ResetConsole();
                return;
            }
            _consoleText.text = text;
        }
    }
}
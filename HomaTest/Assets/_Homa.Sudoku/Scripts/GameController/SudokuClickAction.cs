using _Homa.Library.Scripts;
using _Homa.Library.Scripts.Patterns;

namespace _Homa.Sudoku.Scripts.GameController
{
    public class SudokuClickAction : Singleton<SudokuClickAction>
    {
        public int SelectedNumber { get; private set; } = 0;
    
        public void SetSelectedNumber(int selectedNumber)
        {
            SelectedNumber = selectedNumber;
        }
    }
}

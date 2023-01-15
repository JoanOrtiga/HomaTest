using _Homa.Sudoku.Scripts.LevelData;

namespace _Homa.Sudoku.Scripts.Cell
{
    public interface ICellView
    {
        void Initialize(SudokuCellData sudokuCellData);
        void ShowData();
    }
}
using UnityEngine;

namespace _Homa.Sudoku.Scripts.SudokuStatus.TimeStat
{
    public abstract class SudokuTimeView : MonoBehaviour
    {
        public abstract void SetTime(float seconds);
    }
}
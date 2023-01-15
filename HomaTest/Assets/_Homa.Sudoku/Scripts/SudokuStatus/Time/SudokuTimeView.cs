using System;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.SudokuStatus.Time
{
    public abstract class SudokuTimeView : MonoBehaviour
    {
        public abstract void SetTime(DateTime dateTime);
    }
}
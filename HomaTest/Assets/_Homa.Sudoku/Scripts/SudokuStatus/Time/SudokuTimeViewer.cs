using System;
using TMPro;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.SudokuStatus.Time
{
    public class SudokuTimeViewer : SudokuTimeView
    {
        [SerializeField] private TextMeshProUGUI _timer;
    
        public override void SetTime(DateTime dateTime)
        {
            _timer.text = dateTime.ToShortDateString();
        }
    }
}
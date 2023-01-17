using _Homa.Library.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.SudokuStatus.TimeStat
{
    public class SudokuTimeUIViewer : SudokuTimeView
    {
        [SerializeField] private TextMeshProUGUI _timer;
    
        public override void SetTime(float seconds)
        {
            _timer.text = seconds.SecondsToFormattedTimeEM();
        }
    }
}
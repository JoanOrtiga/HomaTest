using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Homa.Sudoku.Scripts.SudokuStatus.TimeStat
{
    public class SudokuTime : MonoBehaviour
    {
        [SerializeField] private SudokuTimeView sudokuTimeview;

        private float _timeInSeconds;

        private void Awake()
        {
            enabled = false;
        }

        public void StartLevel()
        {
            enabled = true;
            _timeInSeconds = 0;
        }

        public void EndLevel()
        {
            enabled = false;
        }

        private void Update()
        {
            _timeInSeconds += Time.deltaTime;
            sudokuTimeview.SetTime(_timeInSeconds);
        }
    }
}
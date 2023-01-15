using System;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.SudokuStatus.Mistakes
{
    public class MistakesDisplayUI : MonoBehaviour
    {
        [SerializeField] private MistakeUIElement[] _mistakesObjects;

        public void Setup(int totalPossibleMistakes)
        {
            ResetStatus();
            
            for (int i = 0; i < totalPossibleMistakes; i++)
            {
                _mistakesObjects[i].SetActive(true);
            }
        }

        public void ShowMistake(int currentMistakes)
        {
            _mistakesObjects[currentMistakes-1].MarkAsMistake();
        }

        public void ResetStatus()
        {
            foreach (var lifeObj in _mistakesObjects)
            {
                lifeObj.SetActive(false);
                lifeObj.ResetStatus();
            }
        }
    }
}

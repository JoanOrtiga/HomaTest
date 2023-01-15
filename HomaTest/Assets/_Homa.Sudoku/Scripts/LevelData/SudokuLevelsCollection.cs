using System.Collections.Generic;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.LevelData
{
    [CreateAssetMenu(menuName = "Sudoku/LevelList", fileName = "LevelList")]
    public class SudokuLevelsCollection : ScriptableObject
    {
        [field: SerializeField] public List<SudokuLevelData> Levels { get; private set; } = new();
    }
}
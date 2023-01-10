using System;
using System.Collections.Generic;
using UnityEngine;

namespace _HomaSudoku.Scripts
{
    [CreateAssetMenu(menuName = "Sudoku/Level", fileName = "LevelData")]
    public class SudokuLevelData : ScriptableObject
    {
        [field: SerializeField] public List<SudokuSlot> Slots { get; private set; }
    }

    [Serializable]
    public class SudokuSlot
    {
        public Vector2 position;
        public SlotData data;
    }
    
    [Serializable]
    public class SlotData
    {
        public int numValue;
    }
}
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Sounds
{
    [CreateAssetMenu(menuName = "Sudoku/Sound", fileName = "Sound", order = 0)]
    public class Sound : ScriptableObject
    {
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public float Volume { get; private set; } = 1;
        [field: SerializeField] public float Pitch { get; private set; } = 1;
    }
}
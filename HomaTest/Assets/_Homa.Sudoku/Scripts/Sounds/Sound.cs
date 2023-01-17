using UnityEngine;

namespace _Homa.Sudoku.Scripts.Sounds
{
    [CreateAssetMenu(menuName = "Sudoku/Sound", fileName = "Sound", order = 0)]
    public class Sound : ScriptableObject
    {
        [field: SerializeField] public string SoundId { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
    }
}
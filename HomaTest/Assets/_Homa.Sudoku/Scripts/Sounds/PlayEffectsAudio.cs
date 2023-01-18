using UnityEngine;

namespace _Homa.Sudoku.Scripts.Sounds
{
    public class PlayEffectsAudio : MonoBehaviour
    {
        [SerializeField] private Sound sound;
        
        public void PlaySound()
        {
            SoundSystem.Instance.PlaySound(sound);       
        }
    }
}
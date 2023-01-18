using System;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Sounds
{
    public class PlayMusicAudio : MonoBehaviour
    {
        [SerializeField] private Sound song;
        [SerializeField] private bool playOnStart;

        private void Start()
        {
            if (!playOnStart)
                return;
            
            PlayMusic();
        }

        public void PlayMusic()
        {
            SoundSystem.Instance.PlayMusic(song);       
        }
    }
}
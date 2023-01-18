using System;
using _Homa.Library.Scripts.Patterns;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Sounds
{
    [RequireComponent(typeof(AudioSource), typeof(AudioSource))]
    public class SoundSystem : Singleton<SoundSystem>
    {
        [SerializeField] private AudioListener audioListener;
        
        private AudioSource soundSource;
        private AudioSource musicSource;

        public void Awake()
        {
            var soundSources = GetComponents<AudioSource>();
            soundSource = soundSources[0];
            musicSource = soundSources[1];

            soundSource.loop = false;
            musicSource.loop = true;

            Settings.Instance.OnEffectsEnabledChange += SetMuteSounds;
            Settings.Instance.OnMusicEnabledChange += SetMuteMusic;
        }

        /// <summary>
        /// Plays a sound once
        /// </summary>
        /// <param name="sound">Sound to play</param>
        public void PlaySound(Sound sound)
        {
            soundSource.volume = AreSoundsEnabled ? sound.Volume : 0f;
            soundSource.pitch = sound.Pitch;
            soundSource.PlayOneShot(sound.AudioClip);
        }

        /// <summary>
        /// Plays a sound that is looped until a different sound is played
        /// </summary>
        /// <param name="sound">Sound to play</param>
        public void PlayMusic(Sound sound)
        {
            if (sound.AudioClip == null)
            {
                Debug.LogWarning($"Sound \"{sound.name}\" doesn't have AudioClip attached.");
                return;
            }
                
            if (sound.AudioClip == musicSource.clip)
            {
                Debug.LogWarning($"Sound {sound.AudioClip.name} already playing");
                return;
            }
            
            musicSource.clip = sound.AudioClip;
            musicSource.volume = IsMusicEnabled ? sound.Volume : 0f;
            musicSource.pitch = sound.Pitch;
            musicSource.Play();
        }

        public void SetMuteSounds(bool mute)
        {
            AreSoundsEnabled = mute;
            soundSource.volume = AreSoundsEnabled ? 1f : 0f;
        }

        public void SetMuteMusic(bool mute)
        {
            IsMusicEnabled = mute;
            musicSource.volume = IsMusicEnabled ? 0.7f : 0f;
        }

        public bool AreSoundsEnabled { get; private set; } = true;
        public bool IsMusicEnabled { get; private set; } = true;
    }
}
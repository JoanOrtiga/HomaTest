using System;
using _Homa.Library.Scripts.Patterns;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Sounds
{
    [RequireComponent(typeof(AudioSource))]
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

            Settings.Instance.OnEffectsEnabledChange += SetSoundEffectsEnabled;
            Settings.Instance.OnMusicEnabledChange += SetMusicEnabled;
        }

        private void OnDisable()
        {
            Settings.Instance.OnEffectsEnabledChange -= SetSoundEffectsEnabled;
            Settings.Instance.OnMusicEnabledChange -= SetMusicEnabled;
        }

        private void SetSoundEffectsEnabled(bool enabled)
        {
            AreSoundsEnabled = enabled;
        }

        private void SetMusicEnabled(bool enabled)
        {
            IsMusicEnabled = enabled;
        }

        /// <summary>
        /// Plays a sound once
        /// </summary>
        /// <param name="sound">Sound to play</param>
        public void PlaySound(Sound sound, float volume = 1f)
        {
            soundSource.volume = AreSoundsEnabled ? volume : 0f;
            soundSource.pitch = sound.pitch;
            soundSource.PlayOneShot(sound.clip);
        }

        /// <summary>
        /// Plays a sound that is looped until a different sound is played
        /// </summary>
        /// <param name="sound">Sound to play</param>
        public void PlayMusic(Sound sound)
        {
            if (sound.clip == musicSource.clip)
            {
                Debug.LogWarning($"Sound {sound.clip.name} already playing");
                return;
            }
            
            musicSource.clip = sound.clip;
            musicSource.volume = IsMusicEnabled ? sound.volume : 0f;
            musicSource.pitch = sound.pitch;
            musicSource.Play();
        }

        public void SetMuteSounds(bool mute)
        {
            AreSoundsEnabled = mute;
            soundSource.volume = AreSoundsEnabled ? 1f : 0f;
            PlayerPrefs.SetInt(SOUND_PREFS_KEY, AreSoundsEnabled ? 1 : 0);
        }

        public void SetMuteMusic(bool mute)
        {
            IsMusicEnabled = mute;
            musicSource.volume = IsMusicEnabled ? 0.7f : 0f;
            PlayerPrefs.SetInt(MUSIC_PREFS_KEY, IsMusicEnabled ? 1 : 0);
        }

        public bool AreSoundsEnabled { get; private set; } = true;
        public bool IsMusicEnabled { get; private set; } = true;
    }

    public class PlaySound : MonoBehaviour
    {

        public void PlaySound()
        {
            SoundSystem.Instance.Play       
        }
    }
}
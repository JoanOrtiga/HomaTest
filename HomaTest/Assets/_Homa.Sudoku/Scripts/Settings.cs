using System;
using _Homa.Library.Scripts.Patterns;
using _Homa.Sudoku.Scripts.UI;
using _Homa.Sudoku.Scripts.UI.Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Homa.Sudoku.Scripts
{
    public class Settings : Singleton<Settings>
    {
        private const string BatteryModeEnabledKey = "BatteryModeEnabled";
        private const string MusicEnabledKey = "MusicEnabled";
        private const string EffectsEnabledKey = "EffectsEnabled";
        
        private const int BatteryModeFrameRate = 30;
        private const int NonBatteryModeFrameRate = 60;

        [SerializeField] private SettingsView settingsView;
        
        private bool batteryModeEnabled;
        public Action<bool> OnBatteryModeEnabledchange;
        private bool musicEnabled;
        public Action<bool> OnMusicEnabledChange;
        private bool effectsEnabled;
        public Action<bool> OnEffectsEnabledChange;

        private void Start()
        {
            LoadSettings();
            ApplySettingsOnStartup();
        }
        
        private void LoadSettings()
        {
            batteryModeEnabled = PlayerPrefs.GetInt(BatteryModeEnabledKey, 1) != 1;
            musicEnabled = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1;
            effectsEnabled = PlayerPrefs.GetInt(EffectsEnabledKey, 1) == 1;
            
            settingsView.SetInitialToggles(musicEnabled, effectsEnabled, batteryModeEnabled);
        }
        
        private void ApplySettingsOnStartup()
        {
            SetBatteryModeEnabled(batteryModeEnabled);
        }

        private void SavePlayerPrefs(bool isEnabled, string key)
        {
            PlayerPrefs.SetInt(key, isEnabled ? 1 : 0);
            PlayerPrefs.Save();
        }
        
        public void SetBatteryModeEnabled(bool isEnabled)
        {
            Application.targetFrameRate = isEnabled ? NonBatteryModeFrameRate : BatteryModeFrameRate;
            OnBatteryModeEnabledchange?.Invoke(isEnabled);
            batteryModeEnabled = isEnabled;
            SavePlayerPrefs(isEnabled, BatteryModeEnabledKey);
        }
        
        public void SetMusicEnabled(bool isEnabled)
        {
            OnMusicEnabledChange?.Invoke(isEnabled);
            musicEnabled = isEnabled;
            SavePlayerPrefs(isEnabled, MusicEnabledKey);
        }
        
        public void SetEffectsEnabled(bool isEnabled)
        {
            OnEffectsEnabledChange?.Invoke(isEnabled);
            effectsEnabled = isEnabled;
            SavePlayerPrefs(isEnabled, EffectsEnabledKey);
        }
    }
}

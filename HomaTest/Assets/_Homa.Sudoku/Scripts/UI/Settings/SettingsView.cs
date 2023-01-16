using System;
using _Homa.Library.Scripts.DOTween;
using DG.Tweening;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.UI.Settings
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private OptionToggleView musicToggleView;
        [SerializeField] private OptionToggleView effectsToggleView;
        [SerializeField] private OptionToggleView batteryModeToggleView;

        [Header("Show animation")]
        [SerializeField] private MoveUI_DOTween _moveUIDoTween;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private CanvasGroup background;
        
        public void Show()
        {
            _moveUIDoTween.gameObject.SetActive(true);
            _moveUIDoTween.onComplete.RemoveListener(SetUnactive);
            _moveUIDoTween.StartAnimation(_endPoint, 0.5f);
            background.DOFade(1, 0.3f);
            background.blocksRaycasts = true;
        }

        public void Hide()
        {
            _moveUIDoTween.onComplete.RemoveListener(SetUnactive);
            _moveUIDoTween.onComplete.AddListener(SetUnactive);
            _moveUIDoTween.StartAnimation(_startPoint, 0.5f);
            background.DOFade(0, 0.3f);
            background.blocksRaycasts = false;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                Hide();
        }

        private void SetUnactive()
        {
            _moveUIDoTween.gameObject.SetActive(false);
            _moveUIDoTween.onComplete.RemoveListener(SetUnactive);
        }
        
        public void SetInitialToggles(bool isMusicOn, bool isEffectsOn, bool isBatteryModeOn)
        {
            musicToggleView.SetToggle(isMusicOn);
            effectsToggleView.SetToggle(isEffectsOn);
            batteryModeToggleView.SetToggle(!isBatteryModeOn);
        }
        
        public void SetBatteryModeEnabled(bool isEnabled)
        {
            Scripts.Settings.Instance.SetBatteryModeEnabled(isEnabled);
        }
        
        public void SetMusicEnabled(bool isEnabled)
        {
            Scripts.Settings.Instance.SetMusicEnabled(isEnabled);
        }
        
        public void SetEffectsEnabled(bool isEnabled)
        {
            Scripts.Settings.Instance.SetEffectsEnabled(isEnabled);
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace _Homa.Sudoku.Scripts.UI.Settings
{
    public class OptionToggleView : MonoBehaviour
    {
        [SerializeField] private ToggleItem toggleOn;
        [SerializeField] private ToggleItem toggleOff;

        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _notSelectedColor;
        [SerializeField] private Color _selectedColorTxt;
        [SerializeField] private Color _notSelectedColorTxt;

        [SerializeField] private UnityEvent<bool> OnToggleChange; 
        
        public void SetToggle(bool isOn)
        {
            OnToggleChange?.Invoke(isOn);
            
            SetSelected(isOn ? toggleOn : toggleOff);
            SetUnselected(isOn ? toggleOff : toggleOn);
        }

        private void SetSelected(ToggleItem toggle)
        {
            toggle.ChangeImageColor_DOTween.PlayAnimation(_selectedColor);
            toggle.ChangeTextMeshProColor_DOTween.PlayAnimation(_selectedColorTxt);
            toggle.ScaleUI_DOTween.ScaleUp();
            toggle.Button.interactable = false;
        }
        
        private void SetUnselected(ToggleItem toggle)
        {
            toggle.ChangeImageColor_DOTween.PlayAnimation(_notSelectedColor);
            toggle.ChangeTextMeshProColor_DOTween.PlayAnimation(_notSelectedColorTxt);
            toggle.ScaleUI_DOTween.ScaleDown();
            toggle.Button.interactable = true;
        }
    }
}

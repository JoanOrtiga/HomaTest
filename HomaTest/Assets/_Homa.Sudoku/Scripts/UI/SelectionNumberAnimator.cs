using System;
using System.Collections.Generic;
using _Homa.Library.Scripts.DOTween;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.UI
{
    public class SelectionNumberAnimator : MonoBehaviour
    {
        [Serializable]
        private class SelectNumberItem
        {
            public ChangeImageColor_DOTween changeColorButton;
            public ChangeTextMeshProColor_DOTween changeColorText;
            public ScaleUI_DOTween changeScaleButton;
        }
        
        [SerializeField] private Color _notSelectedButton;
        [SerializeField] private Color _selectedButton;
        [SerializeField] private Color _selectedTxt;
        [SerializeField] private Color _notSelectedTxt;

        [SerializeField] private List<SelectNumberItem> _selectNumberItems;

        private int lastSelectedNumber = -1;
        public void SetCurrentItem(int newSelectedNumber)
        {
            newSelectedNumber--;
            
            if (lastSelectedNumber != -1)
            {
                var item = _selectNumberItems[lastSelectedNumber];
                item.changeColorButton.PlayAnimation(_notSelectedButton);
                item.changeScaleButton.ScaleDown();
                item.changeColorText.PlayAnimation(_notSelectedTxt);
            }

            lastSelectedNumber = newSelectedNumber;
            
            var newSelectedItem = _selectNumberItems[newSelectedNumber];
            newSelectedItem.changeColorButton.PlayAnimation(_selectedButton);
            newSelectedItem.changeScaleButton.ScaleUp();
            newSelectedItem.changeColorText.PlayAnimation(_selectedTxt);
        }
    }
}



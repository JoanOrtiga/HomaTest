using System;
using TMPro;
using UnityEngine;

namespace _Homa.Library.Scripts.CommonUIElements.Percentage
{
    public class PercentageViewUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI percentageText;

        public void SetPercentage(int percentage)
        {
            percentageText.text = $"{percentage}%";
        }
        
        public void SetPercentage(float percentage, int shownDecimals = 2)
        {
            percentageText.text = $"{Math.Round(percentage, shownDecimals)}%";
        }
        
        public void SetPercentage(int currentAmount, int totalAmount, bool showDecimals = false, int shownDecimals = 2)
        {
            if (showDecimals)
            {
                var percentage = currentAmount / (float)totalAmount * 100;
                SetPercentage(percentage, shownDecimals);
            }
            else
            {
                var percentage = Mathf.RoundToInt(currentAmount / (float)totalAmount * 100);
                SetPercentage(percentage);
            }
        }
    }
}

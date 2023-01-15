using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _Homa.Sudoku.Scripts.UI
{
    public class ChangeTextMeshProColor_DOTween : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI target;
     
        [SerializeField] private Ease ease;
        [SerializeField] private float duration;
        [SerializeField] private Color endColor;

        [SerializeField] private UnityEvent onComplete;

        public void PlayAnimation()
        {
            target.DOColor(endColor, duration).SetEase(ease).onComplete += () => onComplete?.Invoke();
        }

        public void PlayAnimation(Color targetColor)
        {
            target.DOColor(targetColor, duration).SetEase(ease).onComplete += () => onComplete?.Invoke();
        }
    }
}
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Homa.Sudoku.Scripts.UI
{
    public class ChangeImageColor_DOTween : MonoBehaviour
    {
        [SerializeField] private Image target;
     
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
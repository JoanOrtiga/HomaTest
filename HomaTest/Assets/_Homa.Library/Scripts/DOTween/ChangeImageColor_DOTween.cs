using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Homa.Library.Scripts.DOTween
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

        public void PlayAnimation(Color targetColor, float animDuration = -1)
        {
            if (animDuration <= 0)
            {
                animDuration = duration;
            }
            target.DOColor(targetColor, animDuration).SetEase(ease).onComplete += () => onComplete?.Invoke();
        }
    }
}
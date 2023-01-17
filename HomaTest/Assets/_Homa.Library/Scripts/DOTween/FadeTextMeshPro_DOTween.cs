using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _Homa.Library.Scripts.DOTween
{
    public class FadeTextMeshPro_DOTween : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI target;
        public TextMeshProUGUI Target => target;
     
        [SerializeField] private Ease ease;
        [SerializeField] private float duration;
        [SerializeField] private float endFadeValue;

        [SerializeField] private UnityEvent onComplete;

        public void PlayAnimation()
        {
            target.DOFade(endFadeValue, duration).SetEase(ease).onComplete += () => onComplete?.Invoke();
        }

        public Tween PlayAnimation(float fadeValue, float duration)
        {
            var tween = target.DOFade(fadeValue, duration).SetEase(ease);
            tween.onComplete += () => onComplete?.Invoke();
            return tween;
        }
    }
}
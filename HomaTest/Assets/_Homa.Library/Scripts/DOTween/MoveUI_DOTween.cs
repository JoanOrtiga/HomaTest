using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Homa.Library.Scripts.DOTween
{
    public class MoveUI_DOTween : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        [SerializeField] private Transform startPoint;

        public UnityEvent onComplete;
        
        public void StartAnimation()
        {
            if (startPoint != null)
                transform.position = startPoint.position;
            transform.DOMove(target.position, duration).SetEase(ease);
        }

        public Tween StartAnimation(Transform animEndPoint, float animDuration, Transform animStartPoint = null, Ease overrideEase = Ease.INTERNAL_Zero)
        {
            if (animStartPoint != null)
                transform.position = animStartPoint.position;
            DG.Tweening.DOTween.Kill(transform);
            var tween = transform.DOMove(animEndPoint.position, animDuration)
                .SetEase(overrideEase == Ease.INTERNAL_Zero ? ease : overrideEase);
            tween.onComplete += () => onComplete?.Invoke();
            return tween;
        }
    }
}

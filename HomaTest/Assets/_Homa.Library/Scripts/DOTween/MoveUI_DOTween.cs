using DG.Tweening;
using UnityEngine;

namespace _Homa.Library.Scripts.DOTween
{
    public class MoveUI_DOTween : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        [SerializeField] private Transform startPoint;
    
        public void StartAnimation()
        {
            if (startPoint != null)
                transform.position = startPoint.position;
            transform.DOMove(target.position, duration, false).SetEase(ease);
        }
    }
}

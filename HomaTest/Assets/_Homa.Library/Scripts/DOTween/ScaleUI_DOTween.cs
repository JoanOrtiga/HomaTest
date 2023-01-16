using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Homa.Library.Scripts.DOTween
{
    public class ScaleUI_DOTween : MonoBehaviour , IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Ease scaleUpEase;
        [SerializeField] private Ease scaleDownEase;
        [SerializeField] private Vector3 sizeTarget;
        [SerializeField] private float scaleUpDuration;
        [SerializeField] private float timeBetweenAnimation;
        [SerializeField] private float scaleDownDuration;
        [SerializeField] private Vector3 minScale;
    
        [Header("Triggerers")]
        [SerializeField] private bool click;
        [SerializeField] private bool hoverEnter;
        [SerializeField] private bool hoverExit;

        private Vector3 _initialSize = Vector3.zero;

        private void Start()
        {
            SetInitialSize();
        }

        private void SetInitialSize()
        {
            if(_initialSize == Vector3.zero)
                _initialSize = Vector3.Max(transform.localScale, minScale);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!click)
                return;
        
            PlayFullAnimation();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!hoverEnter)
                return;
        
            ScaleUp();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!hoverExit)
                return;
        
            ScaleDown();
        }

        public void PlayFullAnimation()
        {
            var mySequence = DG.Tweening.DOTween.Sequence();
            mySequence.SetLoops(0);
            mySequence.Append(transform.DOScale(sizeTarget, scaleUpDuration).SetEase(scaleUpEase));
            mySequence.Insert(scaleUpDuration + timeBetweenAnimation,transform.DOScale(_initialSize, scaleDownDuration).SetEase(scaleDownEase));
            mySequence.Play();
        }

        public void ScaleUp()
        {
            SetInitialSize();
            transform.DOScale(sizeTarget, scaleUpDuration).SetEase(scaleUpEase);
        }

        public void ScaleDown()
        {
            SetInitialSize();
            transform.DOScale(_initialSize, scaleDownDuration).SetEase(scaleDownEase);
        }
    }
}

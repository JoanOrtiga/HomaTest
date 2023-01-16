using _Homa.Library.Scripts.DOTween;
using _Homa.Sudoku.Scripts.UI.Settings;
using DG.Tweening;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.UI
{
    public class ShopView : MonoBehaviour
    {
        [Header("Show animation")]
        [SerializeField] private MoveUI_DOTween _moveUIDoTween;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private CanvasGroup background;
        
        public void Show()
        {
            _moveUIDoTween.gameObject.SetActive(true);
            _moveUIDoTween.onComplete.RemoveListener(SetUnactive);
            _moveUIDoTween.StartAnimation(_endPoint, 0.5f);
            background.DOFade(1, 0.3f);
            background.blocksRaycasts = true;
        }

        public void Hide()
        {
            _moveUIDoTween.onComplete.RemoveListener(SetUnactive);
            _moveUIDoTween.onComplete.AddListener(SetUnactive);
            _moveUIDoTween.StartAnimation(_startPoint, 0.5f);
            background.DOFade(0, 0.3f);
            background.blocksRaycasts = false;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                Hide();
        }

        private void SetUnactive()
        {
            _moveUIDoTween.gameObject.SetActive(false);
            _moveUIDoTween.onComplete.RemoveListener(SetUnactive);
        }
    }
}

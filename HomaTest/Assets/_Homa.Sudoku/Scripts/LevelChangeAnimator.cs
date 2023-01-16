using _Homa.Library.Scripts.DOTween;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Homa.Sudoku.Scripts
{
    public class LevelChangeAnimator : MonoBehaviour
    {
        [SerializeField] private MoveUI_DOTween moveUI;
        
        [Header("Anim References")]
        [SerializeField] private Transform targetLeft;
        [SerializeField] private Transform targetMid;
        [SerializeField] private Transform targetRight;
        
        [Header("Anim settings")]
        [SerializeField] private float loseDurationStep1;
        [SerializeField] private float loseDurationStep2;
        [SerializeField] private float winDurationStep1;
        [SerializeField] private float winDurationStep2;
        
        public void LoseLevelAnimation()
        {
            moveUI.onComplete.RemoveListener(LoseLevelAnimationStep2);
            moveUI.onComplete.AddListener(LoseLevelAnimationStep2);
            moveUI.StartAnimation(targetLeft, loseDurationStep1);
        }

        private void LoseLevelAnimationStep2()
        {
            moveUI.onComplete.RemoveListener(LoseLevelAnimationStep2);
            moveUI.onComplete.RemoveListener(LoseLevelAnimationStep3);
            moveUI.onComplete.AddListener(LoseLevelAnimationStep3);
            moveUI.StartAnimation(targetMid, loseDurationStep2);
        }

        private void LoseLevelAnimationStep3()
        {
            moveUI.onComplete.RemoveListener(LoseLevelAnimationStep3);
        }

        public void NextLevelAnimation()
        {
            moveUI.onComplete.RemoveListener(NextLevelAnimationStep2);
            moveUI.onComplete.AddListener(NextLevelAnimationStep2);
            moveUI.StartAnimation(targetRight, winDurationStep1);
        }

        private void NextLevelAnimationStep2()
        {
            moveUI.onComplete.RemoveListener(NextLevelAnimationStep2);
            moveUI.onComplete.RemoveListener(NextLevelAnimationStep3);
            moveUI.onComplete.AddListener(NextLevelAnimationStep3);
            transform.localPosition = targetLeft.localPosition;
            moveUI.StartAnimation(targetMid, winDurationStep2);
        }

        private void NextLevelAnimationStep3()
        {
            moveUI.onComplete.RemoveListener(NextLevelAnimationStep3);
        }
    }
}

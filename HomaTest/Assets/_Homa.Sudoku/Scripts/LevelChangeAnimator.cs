using _Homa.Library.Scripts.DOTween;
using _Homa.Sudoku.Scripts.Sounds;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Homa.Sudoku.Scripts
{
    public class LevelChangeAnimator : MonoBehaviour
    {
        [SerializeField] private UnityEvent LevelLost;
        [SerializeField] private UnityEvent LevelCompleted;
        
        [SerializeField] private MoveUI_DOTween moveUI;
        [SerializeField] private FadeTextMeshPro_DOTween fadeText;
        [SerializeField] private MoveUI_DOTween moveText;
        [SerializeField] private PlayEffectsAudio winAudio;
        [SerializeField] private PlayEffectsAudio loseAudio;
        [SerializeField] private string winText = "Level complete!";
        [SerializeField] private string loseText = "Level failed!";
        
        [Header("Anim References")]
        [SerializeField] private Transform targetLeft;
        [SerializeField] private Transform targetMid;
        [SerializeField] private Transform targetRight;
        [SerializeField] private Transform textInitPoint;
        [SerializeField] private Transform textMidPoint;
        [SerializeField] private Transform textEndPoint;
        
        [Header("Anim settings")]
        [SerializeField] private float loseDurationStep1;
        [FormerlySerializedAs("loseWaitduration")] [SerializeField] private float loseWaitDuration;
        [SerializeField] private float loseDurationStep2;
        [SerializeField] private float winDurationStep1;
        [SerializeField] private float winWaitDuration;
        [SerializeField] private float winDurationStep2;
        [SerializeField] private Ease winEaseStep1;
        [SerializeField] private Ease winEaseStep2;
        [SerializeField] private Ease loseEaseStep1;
        [SerializeField] private Ease loseEaseStep2;
        [SerializeField] private float appearTextDuration;
        [SerializeField] private float waitTextDuration;
        [SerializeField] private float disappearTextDuration;
        [SerializeField] private float delayTextAppearance;
        
        
        public void LoseLevelAnimation()
        {
            loseAudio.PlaySound();
            moveUI.onComplete.RemoveListener(LoseLevelAnimationStep2);
            moveUI.onComplete.AddListener(LoseLevelAnimationStep2);
            moveUI.StartAnimation(targetLeft, loseDurationStep1, overrideEase:loseEaseStep1);
            
            PlayTextAnimation(loseText);
        }

        private void LoseLevelAnimationStep2()
        {
            moveUI.onComplete.RemoveListener(LoseLevelAnimationStep2);
            moveUI.onComplete.RemoveListener(LoseLevelAnimationStep3);
            moveUI.onComplete.AddListener(LoseLevelAnimationStep3);
            moveUI.StartAnimation(targetMid, loseDurationStep2, overrideEase:loseEaseStep2).SetDelay(loseWaitDuration);
            
            LevelLost?.Invoke();
        }

        private void LoseLevelAnimationStep3()
        {
            moveUI.onComplete.RemoveListener(LoseLevelAnimationStep3);
        }

        public void LevelCompleteAnimation()
        {
            winAudio.PlaySound();
            moveUI.onComplete.RemoveListener(LevelCompleteAnimationStep2);
            moveUI.onComplete.AddListener(LevelCompleteAnimationStep2);
            moveUI.StartAnimation(targetRight, winDurationStep1, overrideEase:winEaseStep1);

            PlayTextAnimation(winText);
        }

        private void LevelCompleteAnimationStep2()
        {
            moveUI.onComplete.RemoveListener(LevelCompleteAnimationStep2);
            moveUI.onComplete.RemoveListener(LevelCompletedAnimationStep3);
            moveUI.onComplete.AddListener(LevelCompletedAnimationStep3);
            transform.position = targetLeft.position;
            moveUI.StartAnimation(targetMid, winDurationStep2, overrideEase:winEaseStep2).SetDelay(winWaitDuration);
            
            LevelCompleted?.Invoke();
        }

        private void LevelCompletedAnimationStep3()
        {
            moveUI.onComplete.RemoveListener(LevelCompletedAnimationStep3);
        }

        private void PlayTextAnimation(string text)
        {
            fadeText.Target.text = text;

            fadeText.Target.transform.localPosition = textInitPoint.localPosition;

            var mySequence = DOTween.Sequence();
            mySequence.Append(fadeText.PlayAnimation(1, appearTextDuration));
            mySequence.Insert(0, moveText.StartAnimation(textMidPoint, appearTextDuration));
            mySequence.Insert(appearTextDuration + waitTextDuration, fadeText.PlayAnimation(0, disappearTextDuration));
            mySequence.Insert(appearTextDuration + waitTextDuration, moveText.StartAnimation(textEndPoint, disappearTextDuration));
            mySequence.Play().SetDelay(delayTextAppearance);
        }
    }
}

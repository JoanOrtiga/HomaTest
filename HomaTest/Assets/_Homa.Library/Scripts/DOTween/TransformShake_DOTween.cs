using DG.Tweening;
using UnityEngine;

namespace _Homa.Library.Scripts.DOTween
{
    public class TransformShake_DOTween : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [Header("Animation settings")] 
        [SerializeField] private float duration;

        [SerializeField] private float strength = 1f;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float randomness = 90;
        [SerializeField] private bool snapping = false;
        [SerializeField] private bool fadeOut = true;
        [SerializeField] private ShakeRandomnessMode shakeRandomnessMode = ShakeRandomnessMode.Full;

        [SerializeField] private bool phoneVibration = true;
        
        public void PlayAnimation()
        {
            target.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut, shakeRandomnessMode);
            if (phoneVibration)
            {
                Vibration.VibratePop ();
            }
        }
    }
}
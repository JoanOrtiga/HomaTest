using DG.Tweening;
using UnityEngine;

namespace _Homa.Library.Scripts.DOTween
{
    public class CameraShake_DOTween : MonoBehaviour
    {
        [SerializeField] private Camera target;

        [Header("Animation settings")] 
        [SerializeField] private float duration;

        [SerializeField] private float strength = 3f;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float randomness = 90;
        [SerializeField] private bool fadeOut = true;
        [SerializeField] private ShakeRandomnessMode shakeRandomnessMode = ShakeRandomnessMode.Full;

        public void PlayAnimation()
        {
            target.DOShakePosition(duration, strength, vibrato, randomness, fadeOut, shakeRandomnessMode);
        }
    }
}

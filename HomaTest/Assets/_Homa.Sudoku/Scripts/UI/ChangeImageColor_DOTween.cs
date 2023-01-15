﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Homa.Sudoku.Scripts.UI
{
    public class ChangeImageColor_DOTween : MonoBehaviour
    {
        [SerializeField] private Image target;
     
        [SerializeField] private Ease ease;
        [SerializeField] private float duration;
        [SerializeField] private Color endColor;

        [SerializeField] private UnityEvent OnComplete;
        
        public void PlayAnimation()
        {
            target.DOColor(endColor, duration).SetEase(ease).onComplete += OnComplete.Invoke;
        }
        
        public void PlayAnimation(Color targetColor)
        {
            target.DOColor(targetColor, duration).SetEase(ease).onComplete += OnComplete.Invoke;
        }
    }
}
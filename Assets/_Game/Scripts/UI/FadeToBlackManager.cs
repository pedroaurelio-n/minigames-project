using System;
using DG.Tweening;
using UnityEngine;

public class FadeToBlackManager : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeToBlackObject;

    FadeTransitionOptions Options => GameGlobalOptions.Instance.FadeTransitionOptions;
    
    public void FadeIn (Action completeCallback)
    {
        DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 1, Options.Duration)
            .OnComplete(() => completeCallback?.Invoke());
    }
    
    public void FadeOut (Action completeCallback)
    {
        fadeToBlackObject.alpha = 1;
        DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 0, Options.Duration)
            .OnComplete(() => completeCallback?.Invoke());
    }
}
using System;
using DG.Tweening;
using UnityEngine;

public class FadeToBlackManager : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeToBlackObject;

    FadeTransitionOptions Options => GameGlobalOptions.Instance.FadeTransitionOptions;
    
    public void FadeIn (Action completeCallback, bool revertToZero = false)
    {
        DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 1, Options.Duration)
            .OnComplete(() =>
                {
                    completeCallback?.Invoke();
                    if (revertToZero)
                        fadeToBlackObject.alpha = 0;
                }
            );
    }
    
    public void FadeOut (Action completeCallback)
    {
        fadeToBlackObject.alpha = 1;
        DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 0, Options.Duration)
            .OnComplete(() =>
                {
                    completeCallback?.Invoke();
                }
            );
    }
}
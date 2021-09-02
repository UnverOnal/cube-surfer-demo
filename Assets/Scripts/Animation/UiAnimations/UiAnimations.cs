using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class UiAnimations : MonoBehaviour
{
    public static void Move(RectTransform transformToMove, Vector3 targetPoint, float time, Ease ease)
    {
        transformToMove.DOLocalMove(targetPoint, time).SetEase(ease);
    }

    ///<summary>
    ///Also scales. Uses local values.
    ///</summary>
    public static void Move(RectTransform transformToMove, Vector3 targetPoint, float time, Ease ease, float targetScale, bool destroyOnComplete)
    {
        transformToMove.DOLocalMove(targetPoint, time).SetEase(ease);
        transformToMove.DOScale(targetScale, time).SetEase(ease).OnComplete(() => 
        {
            transformToMove.gameObject.SetActive(!destroyOnComplete);
            transformToMove.SetParent(null);   
        });
    }

    public static void Scale(RectTransform transformToScale, float targetScale, float time)
    {
        transformToScale.DOScale(Vector3.one * targetScale, time).SetEase(Ease.OutBack);
    }
}

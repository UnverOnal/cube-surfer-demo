using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Panel : MonoBehaviour
{
    public CanvasGroup canvasGroup
    {
        get
        {
            return GetComponent<CanvasGroup>();
        }
    }

    public void Enable()
    {
        canvasGroup.alpha = 1f;
        gameObject.SetActive(true);
    }

    public void EnableSmoothly()
    {
        float alpha = canvasGroup.alpha;

        DOTween.To(() => alpha, x => alpha = x, 1f, 0.5f).OnUpdate(() => 
        {
            canvasGroup.alpha = alpha;
        }).
        OnComplete(() => gameObject.SetActive(true));
    }

    public void Disable()
    {
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false); 
    }

    public void DisableSmoothly()
    {
        float alpha = canvasGroup.alpha;

        DOTween.To(() => alpha, x => alpha = x, 0f, 0.5f).OnUpdate(() => 
        {
            canvasGroup.alpha = alpha;
        }).
        OnComplete(() => gameObject.SetActive(false));
    }
}

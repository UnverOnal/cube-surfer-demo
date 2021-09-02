using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PointTextCreator : MonoBehaviour
{
    [HideInInspector]public Vector3 offset;

    public void CreatePointText(Vector3 position, Transform parent)
    {
        GameObject pointText = PoolManager.instance.pointTextPool.GetPooledObject();
        pointText.transform.SetParent(parent);

        pointText.SetActive(true);

        pointText.transform.position = position + offset;
        pointText.transform.localScale *= 0f;

        pointText.transform.DOScale(1f, 0.25f).SetLoops(2 , LoopType.Yoyo).OnComplete(() => 
        {
            pointText.SetActive(false);
            pointText.transform.SetParent(null);
        });
    }
}

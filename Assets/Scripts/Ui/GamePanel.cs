using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GamePanel : Panel
{
    [SerializeField]private Text diamondText;

    [SerializeField]private RectTransform diamondParent;

    [SerializeField]private float diamondAnimationDuration = 3f;

    [SerializeField]private Vector3 startingScale;

    [HideInInspector]public int currentDiamondCount;


    public void CreateDimondUiEffect(Vector3 startingPosition)
    {
        GameObject diamond = PoolManager.instance.diamondUiPool.GetPooledObject();
        RectTransform diamondTransform = diamond.GetComponent<RectTransform>();
        
        diamondTransform.SetParent(diamondParent.parent);

        diamondTransform.position = startingPosition;
        diamondTransform.localScale = startingScale;
        diamondTransform.gameObject.SetActive(true);

        UiAnimations.Move(diamondTransform ,diamondParent.localPosition, diamondAnimationDuration, Ease.InOutBack, diamondParent.lossyScale.x, true);
    }

    //Also displays and calculates diamonds that have been collected in current level.
    public void UpdateDiamondCount()
    {
        currentDiamondCount++;

        int totalDiamonds = GameManager.instance.totalDiamonds + currentDiamondCount;
        GameManager.instance.data.SetMoney(totalDiamonds);

        diamondText.text = totalDiamonds.ToString();
    }

    public void DisplayTotalDiamonds()
    {
        int totalDiamonds = GameManager.instance.totalDiamonds;

        diamondText.text = totalDiamonds.ToString();
    }
}

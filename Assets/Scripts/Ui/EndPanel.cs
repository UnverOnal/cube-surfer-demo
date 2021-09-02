using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : Panel
{
    [SerializeField]private GameObject failPage;
    [SerializeField]private GameObject successPage;

    [Header("Fail")]
    [SerializeField]private Vector3 failTextTargetPoint;
    [SerializeField]private float failAnimationDuration;
    [SerializeField]private RectTransform failTextParent;
    [SerializeField]private RectTransform retryButtonTransform;

    [Header("Success")]
    [SerializeField]private RectTransform diamondParentTransform;
    [SerializeField]private RectTransform winTextParent;
    [SerializeField]private RectTransform nextButtonTransform;
    [SerializeField]private float successAnimationDuration;
    [SerializeField]private Vector3 successTextTargetPoint;
    [SerializeField]private Text diamondText;
    [SerializeField]private Text multiplierText;

    public void EnableSuccessPage()
    {
        successPage.SetActive(true);
        failPage.SetActive(false);

        FillSuccessPage();

        Invoke("MoveTextSuccess", 0.5f);
        Invoke("PopupDiamondCount", 1f);
        Invoke("PopupButtonSuccess", 1.5f);
    }
    public void EnableFailPage()
    {
        successPage.SetActive(false);
        failPage.SetActive(true);

        Invoke("MoveTextFail", 0.5f);
        Invoke("PopupButtonFail", 1f);
    }


    #region Fail
    private void MoveTextFail()
    {
        UiAnimations.Move(failTextParent, failTextTargetPoint, failAnimationDuration, DG.Tweening.Ease.OutBack);
    }

    private void PopupButtonFail()
    {
        UiAnimations.Scale(retryButtonTransform, 0.9f, failAnimationDuration);
    }

    public void RetryButton()
    {
        GameManager.instance.RestartGame();
    }
    #endregion

    #region Success
    private void FillSuccessPage()
    {
        int multiplier = PlayerManager.instance.baseCubeCollisionData.currentMultiplier;
        multiplierText.text = multiplier.ToString() + "X";

        int levelDiamonds = UiManager.instance.gamePanel.currentDiamondCount * multiplier;
        diamondText.text = levelDiamonds.ToString();

        int totalDiamonds = GameManager.instance.totalDiamonds + levelDiamonds;
        GameManager.instance.data.SetMoney(totalDiamonds);
    }

    private void MoveTextSuccess()
    {
        UiAnimations.Move(winTextParent, successTextTargetPoint, successAnimationDuration, DG.Tweening.Ease.OutBack);
    }

    private void PopupButtonSuccess()
    {
        UiAnimations.Scale(nextButtonTransform, 0.9f, successAnimationDuration);
    }

    private void PopupDiamondCount()
    {
        UiAnimations.Scale(diamondParentTransform, 1f, successAnimationDuration);
    }

    public void NextButton()
    {
        GameManager.instance.NextLevel();
    }
    #endregion
}

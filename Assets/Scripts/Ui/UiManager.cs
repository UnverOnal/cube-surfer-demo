using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public MainPanel mainPanel;
    public GamePanel gamePanel;
    public EndPanel endPanel;
    public TutorialPanel tutorialPanel;

    [Header("Point Text")]
    public PointTextCreator pointTextCreator;
    public Vector3 positionOffset;

    private void Awake() 
    {
        //Singleton
        if(instance == null)
            instance = this as UiManager;
        else
            Destroy(gameObject);
    }

    private void Start() 
    {
        GameManager.instance.onStart.AddListener(StartGame);
        GameManager.instance.onSuccess.AddListener(Success);
        GameManager.instance.onFail.AddListener(Fail);

        mainPanel.Enable();

        pointTextCreator.offset = positionOffset;

        gamePanel.DisplayTotalDiamonds();
    }

    private void StartGame()
    {
        mainPanel.Disable();

        if(GameManager.instance.data.IsTutorialShowed)
            gamePanel.EnableSmoothly();
        else
            tutorialPanel.EnableSmoothly();
    }

    private void Success()
    {
        gamePanel.Disable();
        endPanel.EnableSmoothly();

        endPanel.EnableSuccessPage();
    }

    private void Fail()
    {
        gamePanel.Disable();
        endPanel.EnableSmoothly();

        endPanel.EnableFailPage();
    }
}

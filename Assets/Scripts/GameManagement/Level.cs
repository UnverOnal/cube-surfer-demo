using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level
{
    private UnityEvent onStart;
    private UnityEvent onFail;
    private UnityEvent onSuccess;

    private Data data;

    public GameStateInfo gameStateInfo;

    public Level(UnityEvent onStart, UnityEvent onFail, UnityEvent onSuccess, Data data)
    {
        this.onStart = onStart;
        this.onFail = onFail;
        this.onSuccess = onSuccess;
        this.data = data;
    }

    //Also loops the level creation
    public void SpawnLevel(int currentLevel, int totalLevelCount, int levelToLoopFrom, out GameObject levelObject)
    {
        //Level loop
        if(currentLevel % totalLevelCount == 0)
            currentLevel = levelToLoopFrom;
        
        string levelName = "level-" + currentLevel.ToString();

        levelObject = null;
        if(Resources.Load<GameObject>("levels/"+levelName) != null)
            levelObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("levels/"+levelName));
    }

    //Called when the game start.
    public void StartGame()
    {
        onStart.Invoke();

        gameStateInfo.isGameOn = true;    
    }

    //Called when the game is over.
    public void Fail()
    {
        onFail.Invoke();

        gameStateInfo.isGameOn = false;
        gameStateInfo.isFailed = true;
        gameStateInfo.isSuccess = false;
    }

    //Called when the game is completed succesfully.
    public void Success()
    {
        onSuccess.Invoke();

        gameStateInfo.isGameOn = false;
        gameStateInfo.isSuccess = true;
        gameStateInfo.isFailed = false;
    }

    public void LevelUp()
    {
        int currentLevel = GameManager.instance.data.LevelCount;
        data.SetLevel(currentLevel++);
    }

    //State info...
    public struct GameStateInfo
    {
        [HideInInspector]public bool isGameOn;
        [HideInInspector]public bool isFailed;
        [HideInInspector]public bool isSuccess;
    }
}

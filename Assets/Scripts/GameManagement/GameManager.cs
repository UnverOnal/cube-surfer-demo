using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]public UnityEvent onStart = new UnityEvent();
    [HideInInspector]public UnityEvent onFail = new UnityEvent();
    [HideInInspector]public UnityEvent onSuccess = new UnityEvent();

    [HideInInspector]public Level level;

    [HideInInspector]public Data data;

    [Header("Level")]
    public int totalLevelCount;
    public int currentLevel;
    public int levelToLoopFrom;

    [HideInInspector]public GameObject levelObject;

    [HideInInspector]public LevelData levelData;

    [HideInInspector]public int totalDiamonds;

    public ParticleCreator ParticleCreator 
    {
        get
        {
            return GetComponent<ParticleCreator>();
        }
    }

    private void Awake() 
    {
        //Singleton
        if(instance == null)
        {
            instance = this as GameManager;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        //Creates data instance & sets values saved
        data = new Data();
        data.GetData();

        totalDiamonds = data.Money;

        #region LevelSpawning
        level = new Level(onStart, onFail, onSuccess, data);

        currentLevel = data.LevelCount;
        level.SpawnLevel(currentLevel, totalLevelCount, levelToLoopFrom, out levelObject);
        #endregion

        levelData = levelObject.GetComponent<LevelData>();

        //Spawn player
        Instantiate(Resources.Load<GameObject>("player/player"));
    }

    public void RestartGame()
    {
        DreamteckUtility.ResetMovementProgress();

        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        DreamteckUtility.ResetMovementProgress();

        level.LevelUp();
        SceneManager.LoadScene(0);
    }
}

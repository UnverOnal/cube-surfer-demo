using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    private int money;
    public int Money{get{return money;}}
    
    private int levelCount;
    public int LevelCount { get{return levelCount;}}

    private bool isTutorialShowed;
    public bool IsTutorialShowed { get{ return isTutorialShowed; }}

    public void GetData() 
    {
        //Get values saved
        money = PlayerPrefs.GetInt("money", 0);
        levelCount = PlayerPrefs.GetInt("level", 1);
        isTutorialShowed = GetTutorialStatus();
    }

    public void SetMoney(int moneyAmount)
    {
        PlayerPrefs.SetInt("money", moneyAmount);
        PlayerPrefs.Save();
    }

    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.Save();
    }

    public void SetTutorialStatus(bool tutorialStatus)
    {
        int status = tutorialStatus ? 1 : 0 ;

        PlayerPrefs.SetInt("tutorialStatus", status);
        PlayerPrefs.Save();
    }

    private bool GetTutorialStatus()
    {
        bool status = PlayerPrefs.GetInt("tutorialStatus", 0) == 1 ? true : false;

        return status;
    }
}

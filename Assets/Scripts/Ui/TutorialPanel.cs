using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : Panel
{
    public void GotItButton()
    {
        GameManager.instance.data.SetTutorialStatus(true);

        Disable();
        UiManager.instance.gamePanel.EnableSmoothly();
    }
}

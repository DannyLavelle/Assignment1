using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningToLevelSelect : MonoBehaviour
{
    public GameObject LevelSelectPanel;
    public int LevelToLoad;

   public void ReturnToLevelSelectButton()
    {
        LevelSelectPanel.SetActive(true);
        gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateLevelScores : MonoBehaviour
{
    public TMP_Text Level1Time;
    public TMP_Text Level1Rank;
    public TMP_Text Level2Time;
    public TMP_Text Level2Rank;


    public void UpdateLevel1()
    {
        int Level1Complete = PlayerPrefs.GetInt("Level2Unlocked");
        if (Level1Complete == 1)
        {
            Level1Time.text = ("Time Elapsed: " + PlayerPrefs.GetFloat("Level1Time"));
            Level1Rank.text = ("Rank: " + PlayerPrefs.GetString("Level1Score"));
        }
           
        
    }
}

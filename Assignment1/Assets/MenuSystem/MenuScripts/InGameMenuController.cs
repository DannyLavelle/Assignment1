//using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject StartPanel;
    public GameObject PausePanel;
    public GameObject DeathPanel;
   public GameObject WinPanel;
    public GameObject HUD;
    public GameObject TimeOutPanel;
    public GameObject TimerPanel;
    public TMP_Text TimerText;
    public GameObject EnemyInfoPanel;
    public GameObject GoalInfoPanel;
    [SerializeField] 
    bool IsPauseMenuAvailable = false;
    public float totalTime = 60f; 
    private float timeRemaining;
    [HideInInspector] public static bool IsGamePaused = false;
    public int CurrentLevel;

    public TMP_Text TimeElapsed;
    public TMP_Text Score;
    public float SRankTimeRemaining ;
    public float ARankTimeRemaining;
    public float BRankTimeRemaining;
    


    private void Start()
    {
        IsGamePaused = false ;
        Cursor.visible = true;
        StartPanel.SetActive(true);
        PauseControl();
        RemoveHUD();
        Debug.Log("Got through start");
        StartCountdown();
        timeRemaining = totalTime;
    }
    private void Update()
    {
        if (IsPauseMenuAvailable == true)
        {
            CheckPause();
        }

    

    }
    public void OkButton()
    {
        Debug.Log("pressed ok button");
        PauseControl();
        TimerPanel.SetActive(true);
        Cursor.visible = false;
        Debug.Log("Made cursor visible");
    
        Debug.Log("set timescale");
        StartPanel.SetActive(false);
        Debug.Log("Set panel inactive");
    }
   
    private void StartCountdown()
    {
        InvokeRepeating("UpdateCountdown", 1f, 1f); // Update the countdown every second
    }
    private void UpdateCountdown()
    {
        timeRemaining--;
        UpdateTimerDisplay();

        if (timeRemaining <= 0)
        {
            TimeOut();           
            CancelInvoke("UpdateCountdown");         
        }
    }
    private void UpdateTimerDisplay()
    {
        TimerText.text = "Time: " + timeRemaining.ToString("0");
    }
    void PauseControl()//Toggles Pause/Unpause
    {
       switch(IsGamePaused)
        {
            case false:
                Time.timeScale = 0f;
                IsGamePaused = true;
                UnlockCursor();
                RemoveHUD();
                break;
            case true:
                Time.timeScale = 1f;
                IsGamePaused = false;
                LockCursor();
                ActivateHUD();
                break;
        }
    }
    void TimeOut()
    {
        PauseControl();
        TimeOutPanel.SetActive(true);
    }
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void LockCursor()
{
    Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    public void EnemyInfoShow()
    {
        PauseControl();
        EnemyInfoPanel.SetActive(true );

    }
    public void InfoHide()
    {
        PauseControl();
        GoalInfoPanel.SetActive(false );
    }
    void RemoveHUD()
    {
        HUD.SetActive(false);
    }
    void ActivateHUD()
    {
        HUD.SetActive(true);
    }
    public void WinSequence()
    {
        float elapsedTime = (totalTime - timeRemaining);
        PauseControl();
        TimeElapsed.text = ("Time Elapsed: " + elapsedTime.ToString("0"));
        Score.text = ("Rank: " + GetRank());
        WinPanel.SetActive(true);
        SaveScores(elapsedTime);

    }
    public void DeathSequence()
    {
        PauseControl();
        DeathPanel.SetActive(true);
    }
    public void RetryButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Cursor.visible = true;
        SceneManager.LoadScene(currentSceneIndex);
        IsGamePaused = false;
    }
    public bool GetIsGamePaused()
    {
        return IsGamePaused;
    }
    public string GetRank()
    {
        if (timeRemaining < BRankTimeRemaining)
        {
            return "C";
        }
        else if (timeRemaining < ARankTimeRemaining)
        {
            return "B";
        }
        else if (timeRemaining < SRankTimeRemaining)
        {
            return "A";
        }
        else
        {
            return "S";
        }

    }
    public void Returntomenu()
    {
        RemoveHUD();
        SceneManager.LoadScene("Main Menu");
    }
    void SaveScores(float ElapsedTime)
    {
        switch (CurrentLevel)
        {
            case 1:
                if (PlayerPrefs.GetInt("Level2Unlocked") == 1)//check if level had been completed before 
            {
                if (ElapsedTime < PlayerPrefs.GetFloat("Level1Time"))//Check if time beats his pb
                {
                    PlayerPrefs.SetFloat("Level1Time", ElapsedTime);
                    PlayerPrefs.SetString("Level1Score", GetRank());
                }
            }
                else
            {
                PlayerPrefs.SetFloat("Level1Time", ElapsedTime);
                PlayerPrefs.SetString("Level1Score", GetRank());
                PlayerPrefs.SetInt("Level2Unlocked", 1);
            }
        
               
       

            break;
            case 2:
            if (PlayerPrefs.GetInt("Level3Unlocked") == 1)//check if level had been completed before 
            {
                if (ElapsedTime < PlayerPrefs.GetFloat("Level2Time"))//Check if time beats his pb
                {
                    PlayerPrefs.SetFloat("Level2Time", ElapsedTime);
                    PlayerPrefs.SetString("Level2Score", GetRank());    
                }
            }
            else
            {
                PlayerPrefs.SetFloat("Level2Time", ElapsedTime);
                PlayerPrefs.SetString("Level2Score", GetRank());
                PlayerPrefs.SetInt("Level3Unlocked", 1);
            }
            break;
            default:
            break;
        }

        PlayerPrefs.Save();

    }
    public void ResumePause()
    {
        PauseControl();
        PausePanel.SetActive(false);
    }
    void CheckPause()
    {
        if(IsGamePaused == false && Input.GetKeyDown(KeyCode.M))
        {
            PauseControl();
            PausePanel.SetActive(true);

        }
        else if(IsGamePaused == true && Input.GetKeyDown(KeyCode.M) && PausePanel.activeSelf)
        {
            PauseControl();
            PausePanel.SetActive(false);
        }
       
    }


}

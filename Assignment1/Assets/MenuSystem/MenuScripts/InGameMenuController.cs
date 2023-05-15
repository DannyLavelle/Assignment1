//using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject StartPanel;
    public GameObject PausePanel;
   public GameObject WinPanel;
    public GameObject HUD;
    public GameObject TimeOutPanel;
    public GameObject TimerPanel;
    public TMP_Text TimerText;
    public GameObject EnemyInfoPanel;
    [SerializeField] 
    bool IsPauseMenuAvailable = false;
    public float totalTime = 60f; 
    private float timeRemaining;
    [HideInInspector] public static bool IsGamePaused = false;

    private void Start()
    {
        Cursor.visible = true;
        StartPanel.SetActive(true);
        PauseControl();
        Debug.Log("Got through start");
        StartCountdown();
        timeRemaining = totalTime;
    }
    private void Update()
    {

    

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
    public void EnemyInfoHide()
    {
        PauseControl();
        EnemyInfoPanel.SetActive(false );
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
        PauseControl();
        WinPanel.SetActive(true);

    }
}

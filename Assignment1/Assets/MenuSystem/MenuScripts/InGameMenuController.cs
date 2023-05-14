using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject StartPanel;
    public GameObject PausePanel;
   public GameObject WinPanel;
    public GameObject HUD;
    [SerializeField] bool IsPauseMenuAvailable = false;
    [HideInInspector] public static bool IsGamePaused = false;

    private void Start()
    {
        Cursor.visible = true;
        StartPanel.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
        Debug.Log("Got through start");
    }

    public void OkButton()
    {
        Debug.Log("pressed ok button");
        HUD.SetActive(true);

        Cursor.visible = false;
        Debug.Log("Made cursor visible");
        Time.timeScale = 1f;
        IsGamePaused=false;
        Debug.Log("set timescale");
        StartPanel.SetActive(false);
        Debug.Log("Set panel inactive");
    }

}

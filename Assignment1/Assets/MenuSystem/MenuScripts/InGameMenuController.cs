using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject StartPanel;
    public GameObject PausePanel;
   public GameObject WinPanel;
    [SerializeField] bool IsPauseMenuAvailable = false;
    [HideInInspector] public static bool IsGamePaused = false;

    private void Start()
    {
        Cursor.visible = true;
        StartPanel.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void OkButton()
    {
        Cursor.visible = false;
        StartPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}

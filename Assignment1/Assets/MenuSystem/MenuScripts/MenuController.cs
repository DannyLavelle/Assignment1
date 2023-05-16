using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject MainMenuPanel;
    public GameObject LevelSelectPanel;
    public GameObject Level1InfoPage;
    

    private void Start()
    {
        MainMenuPanel.SetActive(true);

    }
    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
    public void Gotolevelselect()
    {
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
    }
    public void GotoMenu()
    {
        MainMenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
        
    }
    public void GotoLevel1InfoPage()
    {
        UpdateLevelScores Scores = gameObject.GetComponent<UpdateLevelScores>();
        LevelSelectPanel.SetActive(false);
        Scores.UpdateLevel1();
        Level1InfoPage.SetActive(true);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
   
   
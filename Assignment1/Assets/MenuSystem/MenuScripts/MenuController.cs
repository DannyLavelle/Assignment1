using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject MainMenuPanel;
    public GameObject LevelSelectPanel;



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
}
   
   
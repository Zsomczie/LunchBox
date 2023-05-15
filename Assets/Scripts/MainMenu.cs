using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]GameObject MainMenuMusic;
    public void playGame()
    {
        Destroy(MainMenuMusic);
        SceneManager.LoadScene(1);
       
    }
    public void back()
    {
        
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();

    }

    public void options()
    {
        
        SceneManager.LoadScene(2);
    }
    public void character()
    {
        SceneManager.LoadScene(3);
    }
}

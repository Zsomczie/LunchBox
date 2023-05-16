using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]GameObject MainMenuMusic;
    bool destroyed = false;
    public void playGame()
    {

        IEnumerator characterMenu()
        {

            yield return new WaitForSeconds(1f);
            DestroyImmediate(MainMenuMusic);
            SceneManager.LoadScene(1);
        }
        StartCoroutine(characterMenu());

    }
    public void back()
    {
        IEnumerator characterMenu()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(0);
        }
        StartCoroutine(characterMenu());

    }

    public void quitGame()
    {

        IEnumerator characterMenu()
        {
            yield return new WaitForSeconds(1f);
            Application.Quit();
        }
        StartCoroutine(characterMenu());

    }

    public void options()
    {
        IEnumerator optionsMenu()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(2);
        }
        StartCoroutine(optionsMenu());
    }
    public void character()
    {
        IEnumerator characterMenu()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(3);
        }
        StartCoroutine(characterMenu());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]GameObject MainMenuMusic;
    public void playGame()
    {

        IEnumerator playMenu()
        {

            yield return new WaitForSeconds(1f);
            DestroyImmediate(MainMenuMusic);
            SceneManager.LoadScene(1);
        }
        StartCoroutine(playMenu());

    }
    public void back()
    {
        IEnumerator backMenu()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(0);
        }
        StartCoroutine(backMenu());

    }

    public void quitGame()
    {

        IEnumerator quitGame()
        {
            yield return new WaitForSeconds(1f);
            Application.Quit();
        }
        StartCoroutine(quitGame());

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

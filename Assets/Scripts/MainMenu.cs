using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : SoundManager
{
    [SerializeField]GameObject MainMenuMusic;
    public Sprite ClassySprite;
    public Sprite FancySprite;
    public GameObject playerPrefab;
    //public SoundManager soundManager;

    public void Start()
    {
        //soundManager.GetComponent<SoundManager>().Load();
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");

    }
    public void playGame()
    {

        IEnumerator playMenu()
        {

            yield return new WaitForSeconds(1f);
            DestroyImmediate(MainMenuMusic);
            Destroy(MainMenuMusic);
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

    public void Classy()
    {

        SpriteRenderer playerSpriteRenderer = playerPrefab.GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = ClassySprite;

        IEnumerator backMenu()
        {
            yield return new WaitForSeconds(1f);
            //SceneManager.LoadScene(0);
        }
        StartCoroutine(backMenu());
    }

    public void Fancy()
    {

        SpriteRenderer playerSpriteRenderer = playerPrefab.GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = FancySprite;

        IEnumerator backMenu()
        {
            yield return new WaitForSeconds(1f);
            //SceneManager.LoadScene(0);
        }
        StartCoroutine(backMenu());
    }



}

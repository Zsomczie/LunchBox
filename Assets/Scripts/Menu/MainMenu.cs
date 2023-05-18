using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class MainMenu : SoundManager
{
    [SerializeField]GameObject MainMenuMusic;
    public Sprite ClassySprite;
    public Sprite FancySprite;
    public GameObject playerPrefab;

    [SerializeField] SpriteResolver hat;
    //public SoundManager soundManager;

    public void Start()
    {
        hat = playerPrefab.GetComponentInChildren<SpriteResolver>();
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
            SceneManager.LoadScene(7);
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
        hat.SetCategoryAndLabel("Hat", "Entry");
        //SpriteRenderer playerSpriteRenderer = playerPrefab.GetComponent<SpriteRenderer>();
        //playerSpriteRenderer.sprite = ClassySprite;

        IEnumerator backMenu()
        {
            yield return new WaitForSeconds(1f);
            //SceneManager.LoadScene(0);
        }
        StartCoroutine(backMenu());
    }

    public void Fancy()
    {

        //SpriteRenderer playerSpriteRenderer = playerPrefab.GetComponent<SpriteRenderer>();
        //playerSpriteRenderer.sprite = FancySprite;
        Debug.Log("geci");
        hat.SetCategoryAndLabel("Hat", "Entry_0");
        Debug.Log("lol");

        IEnumerator backMenu()
        {
            yield return new WaitForSeconds(1f);
            //SceneManager.LoadScene(0);
            
        }
        StartCoroutine(backMenu());
    }



}

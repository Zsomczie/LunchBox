using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour
{
    [Header("Health & Damage")]
    [SerializeField] private float health;
    [SerializeField] private float damage;

    [Header("Game Over")]
    [SerializeField] private SpriteRenderer fadeObject;

    private Shooting shooting;
    private Coroutine currentFire;
    private PlayerController playerController;

    private void Awake()
    {
        shooting = shooting = GameObject.Find("RotatePoint").GetComponent<Shooting>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void TakeDamage()
    {
        health -= shooting.equippedWeapon.damage;

        if(health <= 0)
        {
            Debug.Log("BOSS DEFEATED HURRAY!");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            TakeDamage();

            if(currentFire != null)
            {
                StopCoroutine(OnFire());
            }

            if(collision.gameObject.GetComponent<Bulletmove>().fiery == true)
            {
                currentFire = StartCoroutine(OnFire());
            }

            StartCoroutine(FadeToCredits());

            GetComponent<SpriteRenderer>().enabled = false;
        }  
    }

    private IEnumerator OnFire()
    {
        yield return new WaitForSeconds(0.5f);
        health -= 0.1f;
        yield return new WaitForSeconds(0.5f);
        health -= 0.1f;
        yield return new WaitForSeconds(0.5f);
        health -= 0.1f;
    }

    private IEnumerator FadeToCredits()
    {
        yield return new WaitForSeconds(1.5f);

        bool fading = true;

        Color fadeState = fadeObject.color;

        while (fading)
        {
            fadeState = new Color(0, 0, 0, fadeState.a + 0.01f);
            fadeObject.color = fadeState;
            
            if(fadeObject.color.a >= 1)
            {
                fading = false;
            }

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Credits");
    }
}

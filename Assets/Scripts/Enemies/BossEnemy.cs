using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("Health & Damage")]
    [SerializeField] private float health;
    [SerializeField] private float damage;

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

            Destroy(collision.gameObject);
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
}

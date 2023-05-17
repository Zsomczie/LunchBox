using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbagePlayerDetection : MonoBehaviour
{
    private EnemyCabbage enemyCabbage;
    private PlayerController playerController;

    private void Awake()
    {
        enemyCabbage = GetComponentInParent<EnemyCabbage>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyCabbage.isAttacking)
        {
            if (!playerController.invincible)
            {
                enemyCabbage.DealDamage();
                StartCoroutine(playerInvincible());
            }
            
            enemyCabbage.RestartAttack();
            IEnumerator playerInvincible() 
            {
                playerController.invincible = true;
                yield return new WaitForSeconds(1f);
                playerController.invincible = false;
            }
        }
        if (collision.gameObject.name.Contains("Bullet"))
        {
            enemyCabbage.TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}

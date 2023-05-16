using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbagePlayerDetection : MonoBehaviour
{
    private EnemyCabbage enemyCabbage;

    private void Awake()
    {
        enemyCabbage = GetComponentInParent<EnemyCabbage>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyCabbage.isAttacking)
        {
            enemyCabbage.DealDamage();
        }
        if (collision.gameObject.name.Contains("Bullet"))
        {
            enemyCabbage.TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}

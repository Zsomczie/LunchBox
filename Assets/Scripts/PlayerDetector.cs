using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private Enemy enemyScript;

    void Awake()
    {
        enemyScript = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !enemyScript.playerDetected)
        {
            Debug.Log("player detected");
            enemyScript.playerDetected = true;
            enemyScript.player = collision.gameObject;
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageCollision : MonoBehaviour
{
    private EnemyCabbage enemyCabbage;

    private void Awake()
    {
        enemyCabbage = GetComponentInParent<EnemyCabbage>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("cabbage hit something");

        var speed = enemyCabbage.lastVelocity.magnitude;
        var direction = Vector3.Reflect(enemyCabbage.lastVelocity.normalized, collision.contacts[0].normal);
        enemyCabbage.rb.velocity = direction * Mathf.Max(speed * Time.deltaTime * 30f, 0f);
    }
}

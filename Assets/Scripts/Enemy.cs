using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General Info")]
    [SerializeField] private EnemyType enemyType;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoving;

    [Header("Only for Carrots")]
    [SerializeField] private Vector2 targetPosition;

    //private variables
    private Rigidbody2D rb;
    private BoxCollider2D collider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        isMoving = true;
        StartCoroutine(IdleMovement());
    }

    void Update()
    {
        
    }

    private IEnumerator IdleMovement()
    {
        switch (enemyType)
        {
            case EnemyType.carrot:
                targetPosition = new Vector2(Random.Range(transform.position.x - 5, transform.position.x + 5), Random.Range(transform.position.y - 5, transform.position.y + 5));

                while (isMoving)
                {
                    //transform.position = Vector2.MoveTowards
                }

                break;

            default:
                Debug.Log("No enemy type defined for movement: " + this.gameObject.name);
                break;
        }

        yield return null;
    }
}

public enum EnemyType
{
    carrot,
    broccoli,
    cabbage
}

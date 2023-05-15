using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("General Info")]
    [SerializeField] private EnemyType enemyType;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool onIdle;
    [SerializeField] private Vector3 targetPosition;

    [Header("Player Detection")]
    public bool playerDetected;
    public GameObject player;

    [Header("Attacking")]
    [SerializeField] private bool closeEnoughToAttack;
    [SerializeField] private bool isAttacking;
    [SerializeField] private int damage;

    // general private variables

    private Coroutine currentAttack;
    private Coroutine currentMovementDelay;
    private PlayerController playerController;
    private NavMeshAgent navMeshAgent;


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        SetNewDestination();
        currentMovementDelay = StartCoroutine(DestinationChangeDelay());
    }

    void Update()
    {
        if(!isAttacking)
        {
            DetectPlayer();
        }

        /*if (!isAttacking)
        {
            if (!isMoving && !onIdle)
            {
                //targetPosition = new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), Random.Range(transform.position.y - 2, transform.position.y + 2));
                isMoving = true;
            }

            else if (isMoving)
            {
                DetectPlayer();
                SetNewDestination();
            }
        }*/
    }

    private void SetNewDestination()
    {
        navMeshAgent.SetDestination(new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f)));

        Debug.Log("new destination");

        /*transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if(transform.position == targetPosition)
        {
            onIdle = true;
            isMoving = false;
            StartCoroutine(Idle());
        }*/
    }

    private IEnumerator DestinationChangeDelay()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));

        navMeshAgent.isStopped = true;

        yield return new WaitForSeconds(2f);

        SetNewDestination();
        navMeshAgent.isStopped = false;

        currentMovementDelay = StartCoroutine(AlternativeDestinationChangeDelay());
    }

    private IEnumerator AlternativeDestinationChangeDelay()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));

        navMeshAgent.isStopped = true;

        yield return new WaitForSeconds(2f);

        SetNewDestination();
        navMeshAgent.isStopped = false;

        currentMovementDelay = StartCoroutine(DestinationChangeDelay());
    }

    private void DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 3f, LayerMask.GetMask("Player"));

        if(playerCollider != null)
        {
            Debug.Log("player has been detected");
            player = playerCollider.gameObject;
            playerController = player.GetComponent<PlayerController>();
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            playerDetected = true;
            isAttacking = true;
            StopCoroutine(currentMovementDelay);
            currentAttack = StartCoroutine(CarrotAttack());
            navMeshAgent.isStopped = true;
        }
    }

    IEnumerator CarrotAttack()
    {
        yield return new WaitForSeconds(2f);

        navMeshAgent.isStopped = false;
        Debug.Log("start attacking");
        targetPosition = player.transform.position;

        while (isAttacking)
        {
            targetPosition = player.transform.position;
            navMeshAgent.SetDestination(targetPosition);

            Debug.Log("attackmove");

            if (closeEnoughToAttack)
            {
                DealDamage();
                Debug.Log("Damage done");
                yield break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        Debug.Log("attack ended");
    }

    private void DealDamage()
    {
        // deal damage to player
        playerController.health =- damage;
        //StartCoroutine(Retreat());
        RestartAttack();
    }

    /*IEnumerator Retreat()
    {
        isRetreating = true;
        int retreatCounter = 0;
        float retreatSpeed = 10f;

        while (isRetreating)
        {
            transform.position = Vector2.MoveTowards(transform.position, previousPosition, retreatSpeed * Time.deltaTime);
            retreatCounter++;
            
            /*if(retreatSpeed > 2)
            {
                retreatSpeed =- 0.2f;
            }

            if(retreatCounter >= retreatTime)
            {
                isRetreating = false;
            }

            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(2f);

        Debug.Log("start new attack");

        RestartAttack();
    }*/

    private void RestartAttack()
    {
        StopCoroutine(currentAttack);

        isAttacking = true;

        switch (enemyType)
        {
            case EnemyType.carrot:
                currentAttack = StartCoroutine(CarrotAttack());
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAttacking)
        {
            closeEnoughToAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAttacking)
        {
            closeEnoughToAttack = false;
        }
    }

    private IEnumerator Idle()
    {
        yield return new WaitForSeconds(2f);

        onIdle = false;
    }
}

public enum EnemyType
{
    carrot,
    broccoli,
    cabbage
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCarrot : MonoBehaviour
{
    [Header("General Info")]
    [SerializeField] private EnemyType enemyType;

    [Header("Movement")]
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
    }

    private void SetNewDestination()
    {
        navMeshAgent.SetDestination(new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f)));

        // walk animation here!!

        // maybe turn this into a coroutine to cope with the direction flips if it doesn't work here?
    }

    private IEnumerator DestinationChangeDelay()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));

        // idle animation here!!

        navMeshAgent.isStopped = true;

        yield return new WaitForSeconds(2f);

        SetNewDestination();
        navMeshAgent.isStopped = false;

        currentMovementDelay = StartCoroutine(AlternativeDestinationChangeDelay());
    }

    private IEnumerator AlternativeDestinationChangeDelay()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));

        // idle animation here!!

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

            // spotting animation here!!

            player = playerCollider.gameObject;
            playerController = player.GetComponent<PlayerController>();
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            playerDetected = true;
            isAttacking = true;
            StopCoroutine(currentMovementDelay);
            navMeshAgent.isStopped = true;
            StartCoroutine(SpottingDelayAfterPlayerDetection());

            // increase the speed for upcoming attacks compared to idle roaming
            navMeshAgent.speed = navMeshAgent.speed * 1.7f;
            navMeshAgent.acceleration = navMeshAgent.acceleration * 1.7f;
        }
    }

    private IEnumerator SpottingDelayAfterPlayerDetection()
    {
        yield return new WaitForSeconds(2f);

        currentAttack = StartCoroutine(CarrotAttack());
    }

    IEnumerator CarrotAttack()
    {
        // delay between each attack, can later be removed if needed
        yield return new WaitForSeconds(1f);

        navMeshAgent.isStopped = false;
        targetPosition = player.transform.position;

        // charge animation here!!

        while (isAttacking)
        {

            targetPosition = player.transform.position;
            navMeshAgent.SetDestination(targetPosition);

            // possible animation flips here!!

            if (closeEnoughToAttack)
            {
                DealDamage();
                yield break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    private void DealDamage()
    {
        // attack animation here!!

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
}

public enum EnemyType
{
    carrot,
    broccoli,
    cabbage
}

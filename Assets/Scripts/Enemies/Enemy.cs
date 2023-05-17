using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
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

    [Header("Health")]
    public float Health;

    [Header("For Broccoli Only")]
    [SerializeField] private GameObject kidPrefab;
    [SerializeField] private GameObject babyPrefab;
    [SerializeField] private Transform kidSpawnPoint;
    private string currentState;

    // general private variables

    private Coroutine currentAttack;
    private Coroutine currentMovementDelay;
    private PlayerController playerController;
    private NavMeshAgent navMeshAgent;

    //for damage taking purposes
    private Shooting shooting;

    //Animation stuff
    public Animator enemyAnimator;

    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        shooting = GameObject.Find("RotatePoint").GetComponent<Shooting>();
        if (enemyType == EnemyType.carrot)
        {
            SetNewDestination();
            currentMovementDelay = StartCoroutine(DestinationChangeDelay());
        }

        else if (enemyType == EnemyType.broccoliParent || enemyType == EnemyType.broccoliKid || enemyType == EnemyType.broccoliBaby)
        {
            // instantly make the broccoli kids and babies to attack towards player
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 30f, LayerMask.GetMask("Player"));

            if (playerCollider != null)
            {
                player = playerCollider.gameObject;
                playerController = player.GetComponent<PlayerController>();
                playerDetected = true;
                isAttacking = true;
                navMeshAgent.isStopped = true;

                // spotting animation here!!
                enemyAnimator.SetBool("seePlayer", true);

                StartCoroutine(SpottingDelayAfterPlayerDetection());
            }
        }
        if (gameObject.name.Contains("Parent"))
        {
            Health = 6;
            currentState = "Kid";
        }
        else if (gameObject.name.Contains("Carrot"))
        {
            Health = 4;
        }
        else if (gameObject.name.Contains("Kid"))
        {
            Health = 3;
            currentState = "Baby";
        }
        else if (gameObject.name.Contains("Baby"))
        {
            Health = 1;
            currentState = "Dying";
        }
    }

    void Update()
    {
        if(!isAttacking && enemyType == EnemyType.carrot)
        {
            DetectPlayer();
        }
    }

    private void SetNewDestination()
    {
        navMeshAgent.SetDestination(new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f)));

        // walk animation here!!
        enemyAnimator.SetBool("isWalking", true);

        // maybe turn this into a coroutine to cope with the direction flips if it doesn't work here?
    }

    private IEnumerator DestinationChangeDelay()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));

        // idle animation here!!
        enemyAnimator.SetBool("isWalking", false);

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
        enemyAnimator.SetBool("isWalking", false);

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
            enemyAnimator.SetBool("seePlayer", true);

            player = playerCollider.gameObject;
            playerController = player.GetComponent<PlayerController>();
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

        currentAttack = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
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
        if (playerController.invincible==false)
        {
            playerController.health -= damage;
            RestartAttack();
            StartCoroutine(playerInvincibility());
            
        }
        
        
        //StartCoroutine(Retreat());
        
        IEnumerator playerInvincibility()
        {
            playerController.invincible = true;
            yield return new WaitForSeconds(1f);
            playerController.invincible = false;
        }
    }

    public void TakeDamage()
    {
        Health-=shooting.equippedWeapon.damage;

        if(Health <= 0)
        {
            switch (enemyType)
            {
                case EnemyType.carrot:
                    QuestManager.GetInstance().UpdateQuestProgress(KillQuestTarget.carrot, 1);

                    // death animation here!!
                    enemyAnimator.SetBool("isDead", true);

                    Destroy(gameObject); // ADD DELAY TO THIS SO THAT THE ANIMATION CAN GO THROUG!!
                    break;

                case EnemyType.broccoliParent:
                    int kidsToSpawn = 2;

                    for (int i = 0; i < kidsToSpawn; i++)
                    {
                        SpawnNextBroccoliState("kid", i);
                    }

                    QuestManager.GetInstance().UpdateQuestProgress(KillQuestTarget.broccoli, 1);

                    // death animation here!!
                    enemyAnimator.SetBool("isDead", true);

                    Destroy(gameObject); // ADD DELAY TO THIS SO THAT THE ANIMATION CAN GO THROUG!!
                    break;

                case EnemyType.broccoliKid:
                    int babysToSpawn = 2;

                    for (int i = 0; i < babysToSpawn; i++)
                    {
                        SpawnNextBroccoliState("baby", i);
                    }

                    QuestManager.GetInstance().UpdateQuestProgress(KillQuestTarget.broccoli, 1);

                    // death animation here!!
                    enemyAnimator.SetBool("isDead", true);

                    Destroy(gameObject); // ADD DELAY TO THIS SO THAT THE ANIMATION CAN GO THROUG!!
                    break;

                case EnemyType.broccoliBaby:
                    QuestManager.GetInstance().UpdateQuestProgress(KillQuestTarget.broccoli, 1);

                    // death animation here!!
                    enemyAnimator.SetBool("isDead", true);

                    Destroy(gameObject); // ADD DELAY TO THIS SO THAT THE ANIMATION CAN GO THROUG!!
                    break;

                default:
                    Debug.LogError("No enemy type assigned for " + gameObject.name + ", can't be destroyed!");
                    break;
            }
        }
    }

    private void SpawnNextBroccoliState(string state, int index)
    {
        switch (state)
        {
            case "kid":
                if(index == 0)
                {
                    Instantiate(kidPrefab, kidSpawnPoint.GetChild(0).transform.position, Quaternion.identity);
                }

                else
                {
                    Instantiate(kidPrefab, kidSpawnPoint.GetChild(1).transform.position, Quaternion.identity);
                }

                break;
            case "baby":
                if (index == 0)
                {
                    Instantiate(babyPrefab, kidSpawnPoint.GetChild(0).transform.position, Quaternion.identity);
                }

                else
                {
                    Instantiate(babyPrefab, kidSpawnPoint.GetChild(1).transform.position, Quaternion.identity);
                }
                break;
            default:
                Debug.LogError("No next state for " + gameObject.name + " could be spawned!");
                break;
        }
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

        currentAttack = StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAttacking)
        {
            closeEnoughToAttack = true;
            DealDamage();
        }

        if (collision.gameObject.name.Contains("Bullet"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
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
    broccoliParent,
    broccoliKid,
    broccoliBaby,
    cabbage
}

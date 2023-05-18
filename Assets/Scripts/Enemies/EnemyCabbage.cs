using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCabbage : MonoBehaviour
{
    [Header("General Info")]
    [SerializeField] private EnemyType enemyType;

    [Header("Movement")]
    public float moveSpeed;
    public Vector3 lastVelocity;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool isRolling;

    [Header("Player Detection")]
    [SerializeField] private float detectionRadius;
    public bool playerDetected;
    public GameObject player;

    [Header("Attacking")]
    public bool isRecharging;
    public bool isAttacking;
    [SerializeField] private int damage;

    [Header("Health")]
    public float Health=10;

    [Header("Audio")]
    [SerializeField] private AudioSource cabbageAudio;
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip deathAudio;

    // general private variables
    private PlayerController playerController;
    private Coroutine restartCoroutine;
    private Shooting shooting;
    private SpriteRenderer spriteRenderer;

    [Header("For Script References Only")]
    public Rigidbody2D rb;

    //Animation stuff
    public Animator enemyAnimator;

    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shooting = GameObject.Find("RotatePoint").GetComponent<Shooting>();
        //SetNewDestination();
        //currentMovementDelay = StartCoroutine(DestinationChangeDelay());
    }

    void Update()
    {
        if (!isAttacking)
        {
            DetectPlayer();
        }

        lastVelocity = rb.velocity;

        if(isAttacking && isRolling && !isRecharging && rb.velocity.magnitude < 0.8f)
        {
            isRolling = false;
            rb.velocity = Vector2.zero;
            
            if(restartCoroutine == null)
            {
                StartCoroutine(RestartAttack());
            }
        }
    }

    private void SetNewDestination()
    {
        targetPosition = player.transform.position - transform.position;
        rb.AddForce(targetPosition * Time.deltaTime * moveSpeed * 150f);
        StartCoroutine(IsRollingDelay());

        // walk animation here!!

        // maybe turn this into a coroutine to cope with the direction flips if it doesn't work here?
    }

    private void DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, LayerMask.GetMask("Player"));

        if (playerCollider != null)
        {
            Debug.Log("player has been detected");

            // spotting animation here!!
            enemyAnimator.SetBool("seePlayer", true);

            player = playerCollider.gameObject;
            playerController = player.GetComponent<PlayerController>();
            playerDetected = true;
            isAttacking = true;
            StartCoroutine(SpottingDelayAfterPlayerDetection());
        }
    }

    private IEnumerator SpottingDelayAfterPlayerDetection()
    {
        yield return new WaitForSeconds(2f);

        SetNewDestination();
    }

    private IEnumerator IsRollingDelay()
    {
        yield return new WaitForSeconds(0.2f);

        isRolling = true;
    }

    public void DealDamage()
    {
        // attack animation here!!
        enemyAnimator.SetBool("attackStart", true);

        isRolling = false;
        playerController.health -= damage;
        rb.velocity = Vector2.zero;

        if (restartCoroutine == null)
        {
            StartCoroutine(RestartAttack());
            Debug.Log("restart after damage");
        }
    }

    public void TakeDamage()
    {
        Health -= shooting.equippedWeapon.damage;

        // hit audio here!!
        if (Health>0)
        {
            cabbageAudio.PlayOneShot(hitAudio);
        }

        if (Health<=0)
        {
            //QuestManager.GetInstance().UpdateQuestProgress(KillQuestTarget.cabbage, 1);

            // death animation here!!
            enemyAnimator.SetBool("isDead", true);

            // death audio here!!
            cabbageAudio.PlayOneShot(deathAudio);

            Destroy(gameObject, 1.5f); // ADD DELAY TO THIS SO THAT THE ANIMATION CAN GO THROUG!!
        }
    }

    public IEnumerator RestartAttack()
    {
        isRecharging = true;

        // possible idle animation between attacks here!!

        yield return new WaitForSeconds(2f);

        SetNewDestination();

        isRecharging = false;
    }
}

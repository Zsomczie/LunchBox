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
    public bool playerDetected;
    public GameObject player;

    [Header("Attacking")]
    public bool isRecharging;
    public bool isAttacking;
    [SerializeField] private int damage;

    [Header("Health")]
    public float Health=10;

    // general private variables
    private PlayerController playerController;
    private Coroutine restartCoroutine;
    private Shooting shooting;

    [Header("For Script References Only")]
    public Rigidbody2D rb;


    void Awake()
    {
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
                Debug.Log("restart");
            }
        }
    }

    private void SetNewDestination()
    {
        targetPosition = player.transform.position - transform.position;
        rb.AddForce(targetPosition * Time.deltaTime * moveSpeed * 1500f);
        StartCoroutine(IsRollingDelay());

        // walk animation here!!

        // maybe turn this into a coroutine to cope with the direction flips if it doesn't work here?
    }

    private void DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 5f, LayerMask.GetMask("Player"));

        if (playerCollider != null)
        {
            Debug.Log("player has been detected");

            // spotting animation here!!

            player = playerCollider.gameObject;
            playerController = player.GetComponent<PlayerController>();
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;
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
        yield return new WaitForSeconds(1f);

        isRolling = true;
    }

    public void DealDamage()
    {
        // attack animation here!!


        isRolling = false;
        playerController.health -= damage;
        Debug.Log("Deal damage");
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
        if (Health<=0)
        {
            Destroy(gameObject);
        }// CHANGE THIS TO MATCH THE WEAPON DAMAGE !!
    }

    public IEnumerator RestartAttack()
    {
        isRecharging = true;

        yield return new WaitForSeconds(2f);

        SetNewDestination();

        isRecharging = false;
    }
}

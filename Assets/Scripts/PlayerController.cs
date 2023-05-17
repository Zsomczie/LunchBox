using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2 speed = new Vector2(5, 5);
    public int health = 6;
    public bool invincible;
    public int keys;
    [SerializeField] bool isShielding = false;
    [SerializeField] GameObject shield;
    
    // variable to hold a reference to our SpriteRenderer component
    private SpriteRenderer Player;

    // This function is called just one time by Unity the moment the component loads
    public Animator playerAnimator;

    //variables for animation purposes
    public float horizontalMove;
    public float verticalMove;
    private void Awake()
    {
        gameObject.name = "Player";
        // get a reference to the SpriteRenderer component on this gameObject
        Player = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //idle animation here
        float inputY = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;
        //walking animation here
        horizontalMove = Mathf.Abs(movement.x);
        verticalMove = movement.y;

        if (verticalMove > 0 && horizontalMove <= Mathf.Abs(verticalMove))
        {
            playerAnimator.SetBool("upMove", true);
            playerAnimator.SetBool("sideMove", false);
            playerAnimator.SetBool("downMove", false);
        }
        else if (horizontalMove > 0 && horizontalMove >= Mathf.Abs(verticalMove))
        {
            playerAnimator.SetBool("upMove", false);
            playerAnimator.SetBool("sideMove", true);
            playerAnimator.SetBool("downMove", false);
        }
        else if (verticalMove < 0 && horizontalMove <= Mathf.Abs(verticalMove))
        {
            playerAnimator.SetBool("upMove", false);
            playerAnimator.SetBool("sideMove", false);
            playerAnimator.SetBool("downMove", true);
        }
        else
        {
            playerAnimator.SetBool("upMove", false);
            playerAnimator.SetBool("sideMove", false);
            playerAnimator.SetBool("downMove", false);
        }

        transform.Translate(movement);

        CharacterRotation();
        if (health<=0)
        {
            //dying animation here
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)&&!isShielding)
        {
            StartCoroutine(shielding());
            IEnumerator shielding()
            {
                Debug.Log("lol");
                shield.SetActive(true);
                isShielding = true;
                yield return new WaitForSeconds(2f);
                shield.SetActive(false);
                yield return new WaitForSeconds(5f);
                isShielding = false;
                
            }

        }
        
    }
    void CharacterRotation()
    {
            if (Input.GetKey(KeyCode.A))
            {
                if (Player != null)
                {
                    // flip the sprite
                    //flipping animation here
                    Player.flipX = true;
                }
            }
        if (Input.GetKey(KeyCode.D))
        {
            //reverse flipping animation here
            Player.flipX = false;
        }
    }
}

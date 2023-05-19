using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Bulletmove : MonoBehaviour
{
    [SerializeField] Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    [SerializeField] float speed = 10;
    [SerializeField] Vector3 direction;
    Shooting shooting;
    public bool fiery=false;
    Vector2 velo;
    [SerializeField] SpriteResolver sprite;
    bool fruitFly = false;
    bool rotatedAlready = false;
    [SerializeField] private AudioSource fly;
    [SerializeField] private AudioClip flyShoot;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteResolver>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        shooting = GameObject.Find("RotatePoint").GetComponent<Shooting>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        if (shooting.equippedWeapon.weaponType=="beam")
        {
            sprite.SetCategoryAndLabel("AmmoTypes", "Lemon"+FlameAmmo());
            gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            velo = new Vector2(direction.x + offsetFlame(), direction.y + offsetFlame()).normalized*(speed-5f);
            rb.velocity = velo;
            fiery = true;
            Destroy(gameObject, RandomDestroyTimeBeam());
        }
        else if (shooting.equippedWeapon.weaponType=="shotgun")
        {
            sprite.SetCategoryAndLabel("AmmoTypes", "Corn" + CornAmmo());
            gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            rb.velocity = new Vector2(direction.x + offsetshotgun(), direction.y + offsetshotgun()).normalized * randomizeSpeed();
            Destroy(gameObject, RandomDestroyTimeShotgun());
        }
        else if (shooting.equippedWeapon.weaponName == "Fruitfly")
        {
            
            sprite.SetCategoryAndLabel("AmmoTypes", "Fruitfly" + FruitFlyAmmo());
            fly.PlayOneShot(flyShoot);
            fruitFly = true;
            //rb.velocity = new Vector2(direction.x, direction.y).normalized * randomizeSpeed();
            Destroy(gameObject, 3f);
        }
        else
        {
            sprite.SetCategoryAndLabel("AmmoTypes", shooting.equippedWeapon.weaponName);
            rb.velocity = new Vector2(direction.x + offsetx(), direction.y + offsetx()).normalized * speed;
            Destroy(gameObject, 3f);
        }
        
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fiery)
        {
            
            speed -= 0.03f;

        }
        
    }
    private void Update()
    {
        
        if (fiery)
        {
            rb.velocity = velo.normalized * (speed - 5f);
        }
        if (fruitFly)
        {
            if (rotatedAlready == false)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rotatedAlready = true;

            }
            transform.position=Vector2.MoveTowards(transform.position,shooting.mousePos , 8 * Time.deltaTime);
        }
    }
    float offsetx() 
    {
        return Random.Range(-0.6f, 0.5f);
    }
    float offsetshotgun() 
    {
        return Random.Range(-1.5f, 1.5f);
    }
    float randomizeSpeed() 
    {
        return Random.Range(8f, 10f);
    }
    float offsetFlame()
    {
        return Random.Range(-2.5f, 2.5f);
    }
    int FlameAmmo() 
    {
        return Random.Range(1, 6);
    }
    int CornAmmo()
    {
        return Random.Range(1, 5);
    }
    int FruitFlyAmmo()
    {
        return Random.Range(1, 4);
    }
    float RandomDestroyTimeShotgun() 
    {
        return Random.Range(0.3f, 0.7f);
    }
    float RandomDestroyTimeBeam()
    {
        return Random.Range(0.3f, 1f);
    }
}

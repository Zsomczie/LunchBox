using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletmove : MonoBehaviour
{
    [SerializeField] Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    [SerializeField] float speed = 10;
    [SerializeField] Vector3 direction;
    Shooting shooting;
    public bool fiery=false;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        shooting = GameObject.Find("RotatePoint").GetComponent<Shooting>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        if (shooting.equippedWeapon.weaponType=="beam")
        {
            gameObject.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            rb.velocity = new Vector2(direction.x+offsetFlame(), direction.y+offsetFlame()).normalized * (speed-5f);
            fiery = true;
            Destroy(gameObject, 1f);
        }
        else if (shooting.equippedWeapon.weaponType=="shotgun")
        {
            rb.velocity = new Vector2(direction.x + offsetshotgun(), direction.y + offsetshotgun()).normalized * randomizeSpeed();
            Destroy(gameObject, 0.5f);
        }
        else
        {
            rb.velocity = new Vector2(direction.x + offsetx(), direction.y + offsetx()).normalized * speed;
            Destroy(gameObject, 3f);
        }
        
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        
    }

    // Update is called once per frame
    void Update()
    {
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
        return Random.Range(-3f, 3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletmove : MonoBehaviour
{
    [SerializeField] Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    [SerializeField] float speed = 5;
    [SerializeField] Vector3 direction;
    Shooting shooting;
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
            rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        }
        else
        {
            rb.velocity = new Vector2(direction.x + offsetx(), direction.y + offsetx()).normalized * speed;
        }
        
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    float offsetx() 
    {
        return Random.Range(-0.6f, 0.5f);
    }
}

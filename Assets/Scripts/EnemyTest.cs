using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(0, 0), 5 * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.name.Contains("Bullet"))
        {
            Destroy(this.gameObject);
        }

    }
}

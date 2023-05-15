using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isLocked = true;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            sprite.color = Color.red;

        }
        if (!isLocked)
        {
            sprite.color = Color.black;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Contains("Player")&&!isLocked)
        {
            SceneManager.LoadScene("Room" + GenerateRoom());
        }
    }
    public int GenerateRoom()
    {
        return Random.Range(1, 4);
    }
}

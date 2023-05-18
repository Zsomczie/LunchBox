using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isLocked = true;
    public SpriteRenderer sprite;
    public int roomState = 0;
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
        int roomNumber = 0;

        switch (roomState)
        {
            case 0:
                roomNumber = Random.Range(1, 3);
                break;

            case 1:
                roomNumber = Random.Range(4, 6);
                break;

            case 2:
                roomNumber = 7;
                break;

            default:
                Debug.LogError("This door doesn't know where to go, you are lost. :(");
                break;
        }

        return roomNumber;
    }
}

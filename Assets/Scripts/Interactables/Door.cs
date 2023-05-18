using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isLocked = true;
    public SpriteRenderer sprite;
    public int roomState = 0;

    private int roomNumber;

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
        Debug.Log("trigger enter");

        if (collision.transform.name.Contains("Player")&&!isLocked)
        {
            roomNumber = GenerateRoom();

            if (roomNumber >= 0)
            {
                SceneManager.LoadScene("Room" + roomNumber);
            }

            else
            {
                Debug.Log("Can't return to main room!");
            }
        }
    }
    public int GenerateRoom()
    {
        switch (roomState)
        {
            case -1:
                roomNumber = -1;
                break;
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

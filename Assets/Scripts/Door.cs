using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Contains("Player"))
        {
            SceneManager.LoadScene("Room" + GenerateRoom());
        }
    }
    public int GenerateRoom()
    {
        return Random.Range(1, 4);
    }
}

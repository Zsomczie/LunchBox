using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool IsActiveNow = false;
    public bool pickedup = false;
    PlayerController player;
    Door door;
    Chest chest;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        door = GameObject.Find("Door").GetComponent<Door>();

        GameObject chestObject = GameObject.Find("Chest");

        if(chestObject != null)
        {
            chest = chestObject.GetComponent<Chest>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for healing interactables
        if (Input.GetKeyDown(KeyCode.E) && IsActiveNow && !pickedup&&gameObject.transform.name.Contains("Health") /*&& GameObject.Find("Player").GetComponent<PlayerController>().health < 6*/)
        {
            Debug.Log(gameObject.name);
            pickedup = true;
            player.health++;
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E) && IsActiveNow && !pickedup && gameObject.transform.name.Contains("Key") /*&& GameObject.Find("Player").GetComponent<PlayerController>().health < 6*/)
        {
            Debug.Log(gameObject.name);
            pickedup = true;
            player.keys++;
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E) && IsActiveNow && !pickedup && gameObject.transform.name.Contains("Door")&&player.keys>0 /*&& GameObject.Find("Player").GetComponent<PlayerController>().health < 6*/)
        {
            Debug.Log("door is open");
            door.isLocked = false;
            player.keys--;
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E) && IsActiveNow && !pickedup && gameObject.transform.name.Contains("Chest") && player.keys>0)
        {
            Debug.Log("chest is open");
            chest.isOpen = true;
            player.keys--;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            IsActiveNow = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsActiveNow = false;
    }
}

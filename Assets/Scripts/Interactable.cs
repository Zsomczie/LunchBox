using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool IsActiveNow = false;
    public bool pickedup = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //for healing interactables
        if (Input.GetKeyDown(KeyCode.E) && IsActiveNow && !pickedup&&gameObject.transform.name.Contains("Health") /*&& GameObject.Find("Player").GetComponent<PlayerController>().health < 6*/)
        {
            Debug.Log(gameObject.name);
            pickedup = true;
            GameObject.Find("Player").GetComponent<PlayerController>().health++;
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E) && IsActiveNow && !pickedup && gameObject.transform.name.Contains("Coin") /*&& GameObject.Find("Player").GetComponent<PlayerController>().health < 6*/)
        {
            Debug.Log(gameObject.name);
            pickedup = true;
            GameObject.Find("Player").GetComponent<PlayerController>().money++;
            Destroy(gameObject);
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

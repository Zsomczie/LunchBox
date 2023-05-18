using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public SpriteRenderer chestSpriteRenderer;
    public Sprite openSprite, closedSprite;
    public GameObject Chesties;
    public bool isOpen = false;
    [SerializeField] GameObject droppedWeapon;
    bool itemDropped;

    void Start()
    {
        chestSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            chestSpriteRenderer.sprite = openSprite;
            if (!itemDropped)
            {
                WhenOpen();
                itemDropped = true;
                gameObject.GetComponent<Collider2D>().enabled = false;
            }


        }
        if (!isOpen)
        {
            chestSpriteRenderer.sprite = closedSprite;
        }
    }

    public void WhenOpen()
    {
        Instantiate(droppedWeapon, gameObject.transform.position - new Vector3(0, 1, 0), Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
            
    }
}

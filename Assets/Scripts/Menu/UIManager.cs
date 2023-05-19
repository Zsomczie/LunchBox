using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text KeyText;
    public TMP_Text magText;
    PlayerController player;
    Shooting shooting;
    [SerializeField] List<Sprite> HealthSprites = new List<Sprite>();
    [SerializeField] GameObject heal1, heal2, heal3, heal4;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        shooting = GameObject.Find("RotatePoint").GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    { 
        KeyText.text = player.keys.ToString();
        magText.text = shooting.equippedWeapon.currentAmmo.ToString() + "/" + shooting.equippedWeapon.magCapacity;
        if (player.health==8)
        {
            heal4.GetComponent<Image>().sprite = HealthSprites[2];
        }
        if (player.health == 7)
        {
            heal4.GetComponent<Image>().sprite = HealthSprites[1];
        }
        if (player.health == 6)
        {
            heal4.GetComponent<Image>().sprite = HealthSprites[0];
        }
        if (player.health == 5)
        {
            heal3.GetComponent<Image>().sprite = HealthSprites[1];
        }
        if (player.health == 4)
        {
            heal3.GetComponent<Image>().sprite = HealthSprites[0];
        }
        if (player.health == 3)
        {
            heal2.GetComponent<Image>().sprite = HealthSprites[1];
        }
        if (player.health == 2)
        {
            heal2.GetComponent<Image>().sprite = HealthSprites[0];
        }
        if (player.health == 1)
        {
            heal1.GetComponent<Image>().sprite = HealthSprites[1];
        }
        if (player.health == 0)
        {
            heal1.GetComponent<Image>().sprite = HealthSprites[0];
        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Application.Quit();
        }
    }
}

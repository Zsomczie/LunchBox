using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text HealthText;
    public TMP_Text KeyText;
    public TMP_Text magText;
    PlayerController player;
    Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        shooting = GameObject.Find("RotatePoint").GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = player.health.ToString()+" Health";
        KeyText.text = player.keys.ToString()+ " Keys";
        magText.text = shooting.equippedWeapon.currentAmmo.ToString() + "/" + shooting.equippedWeapon.magCapacity;
    }
}

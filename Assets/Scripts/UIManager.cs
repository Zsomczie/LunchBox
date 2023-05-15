using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text HealthText;
    public TMP_Text KeyText;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = player.health.ToString()+" Health";
        KeyText.text = player.keys.ToString()+ " Keys";
    }
}

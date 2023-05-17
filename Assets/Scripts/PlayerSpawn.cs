using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(player, transform.position,Quaternion.identity);
    }
}

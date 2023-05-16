using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroy : MonoBehaviour
{
     void Start()
    {
        //GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameSound");
        //if (musicObj.Length > 1)
        //{
        //    Destroy(this.gameObject);
        //}
        DontDestroyOnLoad(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    [SerializeField]GameObject credits;
    Vector3 move = new Vector3(0, 5, 0);

    // Update is called once per frame
    void Update()
    {

        credits.transform.Translate(move);

    }
}

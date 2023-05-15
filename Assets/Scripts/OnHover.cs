using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip HoverFX;

    public void HoverSound()
    {
        myFX.PlayOneShot(HoverFX);
    }
   
}

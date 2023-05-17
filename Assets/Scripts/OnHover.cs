using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip HoverFX;
    public AudioClip ClickFX;

    public void HoverSound()
    {
        myFX.PlayOneShot(HoverFX);
    }
    public void ClickSound()
    {

            myFX.PlayOneShot(ClickFX);

    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public GameObject highlightGO;
    public Button button;
    public AudioSource buttonFX;

    public void HandleMouseEnter()
    {
        if (button.IsInteractable())
        {
            highlightGO.SetActive(true);
            buttonFX.mute = false;
        }
        else
        {
            buttonFX.mute = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private void Start()
    {
        DialogueManager.GetInstance().StartDialogue(inkJSON);
    }
}

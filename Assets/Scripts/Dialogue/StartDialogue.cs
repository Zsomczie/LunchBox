using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private void Start()
    {
        StartCoroutine(DelayBeforeDialogue());
    }

    private IEnumerator DelayBeforeDialogue()
    {
        yield return new WaitForSeconds(0.01f);

        DialogueManager.GetInstance().StartDialogue(inkJSON);
    }
}

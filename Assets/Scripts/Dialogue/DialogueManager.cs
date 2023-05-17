using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI potatoNameText;

    [Header("Quest Choices")]
    [SerializeField] private GameObject[] choiceButtons;
    [SerializeField] private TextMeshProUGUI[] choicesText;

    // private variables
    private Story currentStory;
    private bool dialogueIsPlaying;

    private static DialogueManager instance;

    private const string selectedQuest = "selectedQuest";
    private const string potatoName = "potatoName";

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is more than one Dialogue Manager in the scene!");
        }
        
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choiceButtons.Length];
        int index = 0;

        foreach(GameObject choice in choiceButtons)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            choice.SetActive(false);
            index++;
        }
    }

    public void StartDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        Debug.Log("starting dialogue");

        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            HandleTags(currentStory.currentTags);

            DisplayChoices();
        }

        else
        {
            EndDialogue();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        Debug.Log(currentStory.currentChoices.Count);

        if(currentChoices.Count > choiceButtons.Length)
        {
            Debug.LogWarning("There are more choices written than the UI can hold!");
        }

        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choiceButtons[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
            Debug.Log(choice.text);
        }

        for(int i = index; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueDialogue();
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach(string tag in currentTags)
        {
            string[] splitTag = tag.Split(":");

            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case selectedQuest:

                    Quest newQuest = new Quest();

                    switch (tagValue)
                    {
                        case "broccoli":
                            newQuest.target = KillQuestTarget.broccoli;
                            newQuest.neededAmountOfKills = 7;
                            break;

                        case "cabbage":
                            newQuest.target = KillQuestTarget.cabbage;
                            newQuest.neededAmountOfKills = 2;
                            break;

                        case "carrot":
                            newQuest.target = KillQuestTarget.carrot;
                            newQuest.neededAmountOfKills = 6;
                            break;
                    }

                    QuestManager.GetInstance().SetNewQuest(newQuest);

                    break;

                case potatoName:

                    switch (tagValue)
                    {
                        case "talking":
                            potatoNameText.text = "The Talking Potato";
                            break;

                        case "racist":
                            potatoNameText.text = "The Racist Potato";
                            break;
                    }
                    break;
            }
        }
    }

    private void EndDialogue()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        Debug.Log("dialogue ended");

        SceneManager.LoadScene("Main");
    }
}

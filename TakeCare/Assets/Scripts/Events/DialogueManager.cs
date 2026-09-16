using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Activation Settings")]
    [SerializeField] private string dialogueKnotName;

    [SerializeField] private GameEventsManager gameEventsManager;

    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJSON;

    private Story story;

    private bool dialoguePlaying = false;

     // Start is called before the first frame update

    private void Awake()
    {
        story = new Story(inkJSON.text);
    }

    private void Start()
    {
        gameEventsManager = FindObjectOfType<GameEventsManager>();
    }

    // private void ActivateDialogue()
    // {
    //     if (!dialogueKnotName.Equals(string.Empty))
    //     {
    //         GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
    //     }
    // }

   public void OnDialogueButtonClicked()
    {
        if (dialoguePlaying)
        {
            SubmitDialogue();
        }
        else
        {
            EnterDialogue(dialogueKnotName);
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        // GameEventsManager.instance.inputEvents.OnSubmitPressed += SubmitDialogue;
    
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        // GameEventsManager.instance.inputEvents.OnSubmitPressed -= SubmitDialogue;
    }

    private void SubmitDialogue()
    {

        if (!dialoguePlaying)
        {
            return;
        }
        
        ContinueOrExitStory();
    }

    private void EnterDialogue(string knotName)
    {

        // Check if dialogue is already playing
        if (dialoguePlaying)
        {
            return;
        }

        dialoguePlaying = true;

        if (knotName.Equals(dialogueKnotName))
        {
            story.ChoosePathString(knotName);
            Debug.Log("Entered Dialogue: " + knotName);
        }
        else
        {
            Debug.LogWarning("Dialogue knot name does not match: " + knotName);
        }

        ContinueOrExitStory();

    }

    private void ContinueOrExitStory()
    {
        if (story.canContinue)
        {
            string content = story.Continue();
            Debug.Log(content);
        }
        else
        {
            Debug.Log("Dialogue ended.");
            dialoguePlaying = false;

            story.ResetState();
        }
    }

}

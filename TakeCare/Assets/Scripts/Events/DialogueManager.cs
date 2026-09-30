using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Activation Settings")]
    [SerializeField] private string dialogueKnotName;

    [Header("Ink Stories")]
    [SerializeField] private List<TextAsset> inkStories = new List<TextAsset>();

    [Header("Day Settings")]
    [SerializeField] private int dayCount = 1;
    [SerializeField] private TextMeshProUGUI dayCountText;

    [Header("Player Controller")]
    [SerializeField] private PlayerController2D playerController;
    [SerializeField] private DialogueUI dialogueUI;

    private Story story;

    private bool dialoguePlaying = false;

    private const string DayCountKey = "DayCount";
    private static bool firstStarted = false;


    private void Awake()
    {
        // Only reset once when the game is first started
        if (!firstStarted)
        {
            PlayerPrefs.SetInt(DayCountKey, 1);
            PlayerPrefs.Save();

            firstStarted = true;
        }

        dayCount = PlayerPrefs.GetInt(DayCountKey, 1);
        UpdateDayCountText();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetSession()
    {
        firstStarted = false;
    }


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
        if (dialoguePlaying)
        {
            return;
        }

        int storyIndex = dayCount - 1;

        if (storyIndex < 0 || storyIndex >= inkStories.Count)
        {
            Debug.LogWarning("No Ink story assigned for Day " + dayCount);
            return;
        }

        if (inkStories[storyIndex] == null)
        {
            Debug.LogWarning("Ink story for Day " + dayCount + " is empty.");
            return;
        }

        // Load dialogue based on current saved day
        story = new Story(inkStories[storyIndex].text);

        dialoguePlaying = true;

        playerController.DisablePlayerMovement();
        dialogueUI.DialogueStarted();



        if (knotName.Equals(dialogueKnotName))
        {
            story.ChoosePathString(knotName);
            Debug.Log("Entered Day " + dayCount + " Dialogue: " + knotName);
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

            dialogueUI.DisplayDialogue(content);
        }
        else
        {
            ExitDialogue();
        }
    }


    private void ExitDialogue()
    {
        Debug.Log("Day " + dayCount + " dialogue ended.");

        dialoguePlaying = false;

        story.ResetState();

        playerController.EnablePlayerMovement();
        dialogueUI.DialogueFinished();

        // Increase to next day
        dayCount++;

        // Save day so it survives scene changes/reloads
        PlayerPrefs.SetInt(DayCountKey, dayCount);
        PlayerPrefs.Save();

        UpdateDayCountText();

        Debug.Log("Current day is now Day " + dayCount);
    }


    private void UpdateDayCountText()
    {
        if (dayCountText != null)
        {
            dayCountText.text = "Day " + dayCount;
        }
    }
}
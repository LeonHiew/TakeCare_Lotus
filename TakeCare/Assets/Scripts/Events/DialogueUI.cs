using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue UI Settings")]

    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueButton;
    
    private void Awake()
    {
        contentParent.SetActive(false);
        dialogueButton.SetActive(false);

        ResetPanel();
    }

    public void DialogueStarted()
    {
        contentParent.SetActive(true);
        dialogueButton.SetActive(true);
    }

    public void DialogueFinished()
    {
        contentParent.SetActive(false);
        dialogueButton.SetActive(false);

        ResetPanel();
    }

    public void DisplayDialogue(string dialogueLine)
    {
        dialogueText.text = dialogueLine;
    }

    private void ResetPanel()
    {
        dialogueText.text = string.Empty;
    }

    
}

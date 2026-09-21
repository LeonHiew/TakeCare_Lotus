using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueEvents
{
    public event Action<string> onEnterDialogue;

    public void EnterDialogue(string knotName)
    {
        if (onEnterDialogue != null)
        {
            onEnterDialogue.Invoke(knotName);

        }
    }

    public event Action onDialogueStarted;

    public void DialogueStarted() 
    {
        if (onDialogueStarted != null)
        {
            onDialogueStarted();
        }
    }

    public event Action onDialogueEnded;

    public void DialogueEnded()
    {
        if (onDialogueEnded != null)
        {
            onDialogueEnded();
        }
    }

    public event Action<string> onDisplayDialogueLine;

    public void DisplayDialogue(string dialogueLine)
    {
        if (onDisplayDialogueLine != null)
        {
            onDisplayDialogueLine(dialogueLine);
        }
    }
}

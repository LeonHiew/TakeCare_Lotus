using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        dialogueEvents = new DialogueEvents();
    }
}

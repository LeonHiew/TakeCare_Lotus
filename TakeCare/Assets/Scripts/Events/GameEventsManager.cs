using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;
    public DialogueEvents dialogueEvents;


    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        dialogueEvents = new DialogueEvents();
        DontDestroyOnLoad(this.gameObject);
    }


}

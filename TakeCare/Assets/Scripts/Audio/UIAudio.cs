using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    public void PlayHoverSFX()
    {
        AudioManager.Instance.PlayHoverSFX();
    }

    public void PlayHighlightSFX()
    {
        AudioManager.Instance.PlayHighlightSFX();
    }

    public void PlayClickSFX()
    {
        AudioManager.Instance.PlayClickSFX();
    }

    public void PlaySelectSFX()
    {
        AudioManager.Instance.PlaySelectSFX();
    }
}

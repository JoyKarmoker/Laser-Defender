using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEvent : MonoBehaviour
{

    public void ClickSound()
    {
        AudioManager.instance.play(AllStringConstants.BUTTONCLICK_SOUND, false);
    }

    public void BackToMainMenuSound()
    {
        AudioManager.instance.play(AllStringConstants.BACKTOMAINMENU_SOUND, false);
    }

    public void ToggleSound()
    {
        AudioManager.instance.play(AllStringConstants.TOGGLE_SOUND, false);
    }

    public void PlaySound()
    {
        AudioManager.instance.play(AllStringConstants.PLAY_SOUND, false);
    }



}

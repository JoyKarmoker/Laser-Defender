using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEvent : MonoBehaviour
{
    [SerializeField] enum ButtonSoundType
    {
        Click,
        BackToMainMenu,
        Toggle,
        Play
    }
    [SerializeField] ButtonSoundType buttonSoundType;


    private void Start()
    {
        switch(buttonSoundType)
        {
            case ButtonSoundType.Click:
                GetComponent<Button>().onClick.AddListener( ()=> ClickSound());
                break;

            case ButtonSoundType.BackToMainMenu:
                GetComponent<Button>().onClick.AddListener(() => BackToMainMenuSound());
                break;

            case ButtonSoundType.Toggle:
                GetComponent<Toggle>().onValueChanged.AddListener(delegate { ToggleSound(); });
                break;

            case ButtonSoundType.Play:
                GetComponent<Button>().onClick.AddListener(() => PlaySound());
                break;
        }
    }
    public void ClickSound()
    {
        AudioManager.instance.play(AllStringConstants.BUTTONCLICK_SOUND, false, true);
    }

    public void BackToMainMenuSound()
    {
        AudioManager.instance.play(AllStringConstants.BACKTOMAINMENU_SOUND, false, true);
    }

    public void ToggleSound()
    {
        AudioManager.instance.play(AllStringConstants.TOGGLE_SOUND, false, true);
    }

    public void PlaySound()
    {
        AudioManager.instance.play(AllStringConstants.PLAY_SOUND, false, true);
    }



}

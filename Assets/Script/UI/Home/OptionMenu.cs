using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {
    public HomeMenu hMenu;

    public Image ButtonMute;
    public Sprite mutedIcon;
    public Sprite unmutedIcon;

    public void InAnimation()
    {
        GetComponent<Animator>().SetBool("disabled", false);
    }

    public void OutAnimation()
    {
        GetComponent<Animator>().SetBool("disabled", true);
    }

    public void EndOutAnimation()
    {
        hMenu.HideOption();
    }

    public void SoundToggle()
    {
        if (AudioListener.volume == 0)
            UnMute();
        else
            Mute();
    }

    void Mute()
    {
        AudioListener.volume = 0;
        ButtonMute.sprite = mutedIcon;
    }

    void UnMute()
    {
        AudioListener.volume = 1;
        ButtonMute.sprite = unmutedIcon;
    }
}

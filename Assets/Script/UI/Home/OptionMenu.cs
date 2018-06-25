using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour {
    public HomeMenu hMenu;

    public void InAnimation()
    {
        GetComponent<Animator>().SetBool("disabled", false);
    }

    public void OutAnimation()
    {
        GetComponent<Animator>().SetBool("disabled", true);
    }

    public void HideOption()
    {
        hMenu.HideOption();
    }
}

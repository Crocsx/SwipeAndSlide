using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGPause : MonoBehaviour
{
    public InGameUI hMenu;

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
        StageManager.instance.ResumeStage();
    }
}

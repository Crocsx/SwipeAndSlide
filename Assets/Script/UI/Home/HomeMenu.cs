using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public Carousel carousel;
    public GameObject optionPanel;
    public GameObject HomePanel;

    public void Start()
    {
        carousel.Activate();
    }

    // Update is called once per frame
    public void ShowOption()
    {
        carousel.Deactivate();
        optionPanel.SetActive(true);
    }

    public void HideOption()
    {
        carousel.Activate();
        optionPanel.SetActive(false);
    }


    // Update is called once per frame
    public void LoadSelectedStage()
    {
        string eValue = carousel.selected.value;
        if(eValue != "")
        {
            GameManager.instance.LoadScene(eValue);
        }
    }
}

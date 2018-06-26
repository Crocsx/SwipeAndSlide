using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public Carousel carousel;
    public GameObject optionPanel;
    public GameObject homePanel;
    public GameObject creditsPanel;

    public Button optionButton;

    public void Start()
    {
        carousel.Activate();
    }

    public void ShowOption()
    {
        carousel.Deactivate();
        optionButton.interactable = false;
        optionPanel.SetActive(true);
    }

    public void HideOption()
    {
        carousel.Activate();
        optionButton.interactable = true;
        optionPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        creditsPanel.GetComponent<Animator>().SetBool("visible", true);
        creditsPanel.GetComponent<Animator>().Play("Credits_Panel_Slide_In");
    }

    public void HideCredits()
    {
        creditsPanel.GetComponent<Animator>().SetBool("visible", false);
        creditsPanel.GetComponent<Animator>().Play("Credits_Panel_Slide_Out");
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

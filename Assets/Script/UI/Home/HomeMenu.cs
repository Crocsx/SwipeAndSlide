using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public Carousel carousel;

    // Update is called once per frame
    public void LoadSelectedStage()
    {
        string eValue = carousel.GetSelectedPanelValue();
        if(eValue != "")
        {
            GameManager.instance.LoadScene(eValue);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public Carousel carousel;
    public AudioSource audioSource;

    void Start()
    {
        carousel.OnSwipe += OnCarouselSwipe;
        TrailerSong(carousel.selected.songClip);
    }

    void OnCarouselSwipe(int dir, CarouselElement selected)
    {
        TrailerSong(selected.songClip);
    }

    void TrailerSong(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.Play();
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

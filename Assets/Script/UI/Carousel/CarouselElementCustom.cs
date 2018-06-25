using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarouselElementCustom : MonoBehaviour {

    public GameObject SongTitlePanel;
    public ParticleSystem ParticlePanel;
    public AudioSource audioSource;

    Animator SongTitlePanelAnimator;
    Carousel cCarousel;
    CarouselElement cElement;

    bool animDone;

    // Use this for initialization
    void Start ()
    {
        SongTitlePanelAnimator = SongTitlePanel.GetComponent<Animator>();
        cElement = transform.GetComponent<CarouselElement>();
        cElement.OnSlide += OnCarouselSlide;
        cElement.OnSelected += StartPanelAnimations;
        cElement.OnUnselected += StopPanelAnimations;
        SongTitlePanel.SetActive(false);     
    }
	
	// Update is called once per frame
	void OnCarouselSlide(int dir)
    {
        SongTitlePanel.SetActive(true);
        SongTitlePanelAnimator.SetBool("selected", cElement.isSelected);
        SongTitlePanelAnimator.SetInteger("direction", dir);
        animDone = true;
    }

    void TrailerSong(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.Play();
    }

    void Update()
    {
        if (animDone)
        {
            OnEndAnim();
        }
    }

    void OnEndAnim()
    {
        SongTitlePanelAnimator.SetInteger("direction", 0);
        animDone = false;
    }

    void StartPanelAnimations(CarouselElement selected)
    {
        SongTitlePanel.SetActive(true);
        ParticlePanel.Play();
        TrailerSong(cElement.songClip);
    }

    void StopPanelAnimations(CarouselElement selected)
    {
        SongTitlePanel.SetActive(false);
        ParticlePanel.Stop();
        ParticlePanel.Clear();
    }
}

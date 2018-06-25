using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarouselElementCustom : MonoBehaviour {

    public GameObject SongTitlePanel;
    public ParticleSystem ParticlePanel;
    public AudioSource audioSource;

    Animator pAnimator;
    Carousel cCarousel;
    CarouselElement cElement;

    private void OnEnable()
    {
        cElement = transform.GetComponent<CarouselElement>();
        cElement.OnSlide += OnCarouselSlide;
        cElement.OnSelected += StartPanelAnimations;
        cElement.OnUnselected += StopPanelAnimations;
    }

    private void OnDisable()
    {
        cElement = transform.GetComponent<CarouselElement>();
        cElement.OnSlide -= OnCarouselSlide;
        cElement.OnSelected -= StartPanelAnimations;
        cElement.OnUnselected -= StopPanelAnimations;
        StopPanelAnimations(cElement);
    }

    // Use this for initialization
    void Awake ()
    {
        pAnimator = GetComponent<Animator>(); 
    }
	
	// Update is called once per frame
	void OnCarouselSlide(int dir)
    {
        SongTitlePanel.SetActive(true);
        pAnimator.SetBool("selected", cElement.isSelected);
        pAnimator.SetInteger("direction", dir);
    }

    void TrailerSong(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.Play();
    }


    public void OnEndAnim()
    {
        pAnimator.SetInteger("direction", 0);
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

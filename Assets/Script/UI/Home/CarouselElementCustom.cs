using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarouselElementCustom : MonoBehaviour {

    public GameObject SongTitlePanel;
    public Animator SongTitlePanelAnimator;
    public ParticleSystem ParticlePanel;

    Carousel cCarousel;
    CarouselElement cElement;

    bool animDone;

    // Use this for initialization
    void Start ()
    {
        cElement = transform.GetComponent<CarouselElement>();
        SongTitlePanelAnimator = SongTitlePanel.GetComponent<Animator>();
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
    }

    void StopPanelAnimations(CarouselElement selected)
    {
        SongTitlePanel.SetActive(false);
        ParticlePanel.Stop();
        ParticlePanel.Clear();
    }
}

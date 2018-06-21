using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarouselElement : MonoBehaviour {

    Carousel carousel;
    RectTransform rectTrans;
    public int index { get { return _index; } set { _index = value; } }
    int _index;

    void Start () {
        rectTrans = transform.GetComponent<RectTransform>();
    }
	
	void Update () {
		
	}

    public void Setup(Carousel pCarousel, float w, Vector2 pos)
    {
        SetWidth(w);
        SetPosition(pos);
        carousel = pCarousel;
    }

    public void SetWidth(float w)
    {
        rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
    }

    public void SetPosition(Vector2 targetPos)
    {
        rectTrans.anchoredPosition = new Vector2(targetPos.x, targetPos.y);
    }

    public void SlideToPosition(Vector2 targetPos)
    {
        StartCoroutine(Slide(targetPos));
    }

    IEnumerator Slide(Vector2 targetPos)
    {
        float timer = 0;
        Vector2 startPos = rectTrans.anchoredPosition;
        while (timer < carousel.slideTime)
        {
            rectTrans.anchoredPosition = Vector3.Lerp(startPos, targetPos, timer / carousel.slideTime);
            timer += Time.deltaTime;
            yield return null;
        }
        rectTrans.anchoredPosition = targetPos;
    }
}

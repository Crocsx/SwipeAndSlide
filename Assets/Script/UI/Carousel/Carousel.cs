using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour {

    public delegate void onResize(Vector2 screenSize, Vector2 PanelSize);
    public event onResize OnResize;
    public delegate void onSwipe(int dir, CarouselElement selected);
    public event onSwipe OnSwipe;

    public bool isEnable { get { return _enabled; } }
    bool _enabled;
    public CarouselElement selected { get { return _selected; } }
    CarouselElement _selected;
    public float slideTime = 0.3f;

    RectTransform rectTrans;
    Vector2 resolution;

    public CarouselElement[] elements;
    Vector2[] positions;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
    }
    void Start() {
        TouchManager.instance.OnSwipe += SwipeAction;
        resolution = new Vector2(Screen.width, Screen.height);
        SetupCarousel();
    }

    void SwipeAction(TouchStruct touch, Vector2 direction)
    {
        if (!_enabled)
            return;

        Vector2 swipeDirection = ToolBox.instance.ClosestDirection(direction);
        Slide((int)swipeDirection.x);
    }

    void Update()
    {
        CheckResolution();
    }

    /// <summary>
    /// Get size of the container panel
    /// create the points at the good distance 
    /// set width and position of all the carousel elements
    /// </summary>
    void SetupCarousel()
    {
        int l = elements.Length;
        Vector2 pSize = GetPanelSize();
        positions = new Vector2[elements.Length];
        for (int i = 0; i < l; i++)
        {
            positions[i] = new Vector2(pSize.x * (i - 1), rectTrans.anchoredPosition.y);
            elements[i].Setup(this, pSize.x, positions[i]);
            SetElementIndex(elements[i], i);
        }
    }

    /// <summary>
    /// Slide the carousel elements in a direction
    /// If the Panel index is in the user view, make the movement slide
    /// If no just set the position to the new one
    /// </summary>
    /// <param name="value"> 
    ///                      -1 = slide left
    ///                       1 = slide right
    ///  </param>
    public void Slide(int value)
    {
        int l = elements.Length;
        for (int i = 0; i < l; i++)
        {
            // Check if we are in the end of the position array or not
            int newIndex = elements[i].index + value;
            if (newIndex < 0)
                newIndex = (l - 1);
            if (newIndex > (l-1))
                newIndex = 0;

            // This line say basically, if we are the slide going out or the one coming in, then slide smoothly
            // if not, just set the position without animations
            if ((value < 0 && (newIndex == 0 || newIndex == 1)) || 
                (value > 0 && (newIndex == 1 || newIndex == 2)))
            {
                elements[i].SlideToPosition(value, positions[newIndex]);
            }
            else
            {
                elements[i].SetPosition(positions[newIndex]);
            }

            SetElementIndex(elements[i], newIndex);
        }

        if (OnSwipe != null)
            OnSwipe(value, selected);
    }

    /// <summary>
    /// Set the carousel element index
    /// check if the element is now the one active
    /// </summary>
    void SetElementIndex(CarouselElement elem, int index)
    {

        elem.setIndex(index);

        if (elem.isSelected)
            _selected = elem;
    }

    /// <summary>
    /// Activate the Carousel and the swipe elements
    /// </summary>
    public void Activate()
    {
        _enabled = true;
    }

    /// <summary>
    /// Deactivate the Carousel and the swipe elements
    /// </summary>
    public void Deactivate()
    {
        _enabled = false;
    }

    /// <summary>
    /// Check if the resolution of the screen has changed
    /// if so, call OnResize Function and recalibrate carousel
    /// </summary>
    void CheckResolution()
    {
        
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            resolution = new Vector2(Screen.width, Screen.height);
            if(OnResize != null)
            {
                OnResize(resolution, GetPanelSize());
                SetupCarousel();
            }
        }
    }

    /// <summary>
    /// return the size of the Carousel Container Panel 
    /// </summary>
    /// <returns>Vector2 Size Carousel Container panel </returns>
    public Vector2 GetPanelSize()
    {
        return new Vector2(rectTrans.rect.width, rectTrans.rect.height);
    }

    private void OnDestroy()
    {
        TouchManager.instance.OnSwipe -= SwipeAction;
    }
}

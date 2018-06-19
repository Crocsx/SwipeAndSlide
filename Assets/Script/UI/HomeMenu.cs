using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public GameObject[] StagePanels;

    Vector3[] CarouselPosition;
    Dictionary<GameObject, int> StagePanelsDic = new Dictionary<GameObject, int>();

    // Use this for initialization
    void Start () {
        TouchManager.instance.OnSwipe += SwipeAction;
        SetupCarousel();
    }
	
    void SetupCarousel()
    {
        CarouselPosition = new Vector3[StagePanels.Length];
        for (int i = 0; i < StagePanels.Length; i++)
        {
            CarouselPosition[i] = StagePanels[i].transform.position;
            StagePanelsDic.Add(StagePanels[i], i);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    void SwipeAction(TouchStruct touch, Vector2 direction)
    {
        Vector2 swipeDirection = ToolBox.instance.ClosestDirection(direction);
        Debug.Log((int)swipeDirection.x);
        NavigateStage((int)swipeDirection.x);
    }

    void NavigateStage(int value)
    {
        StartCoroutine(SlideStages(value));
    }

    IEnumerator SlideStages(int value)
    {
        float time = 0;
        float slideSpeed = 0.5f;
        while (time < slideSpeed)
        {
            foreach (var item in StagePanelsDic)
            {
                int newValue = item.Value + value;
                if (newValue < 0)
                    newValue = CarouselPosition.Length - 1;
                else if (newValue > CarouselPosition.Length - 1)
                    newValue = 0;

                item.Key.transform.position = Vector3.Slerp(CarouselPosition[item.Value], CarouselPosition[newValue], time / slideSpeed);
            }
            time += Time.deltaTime;
            yield return null;
        }
        foreach (var item in StagePanelsDic)
        {
            int newValue = item.Value + value;
            if (newValue < 0)
                newValue = CarouselPosition.Length - 1;
            else if (newValue > CarouselPosition.Length - 1)
                newValue = 0;
            item.Key.transform.position = CarouselPosition[newValue];
        }
    }

    private void OnDestroy()
    {
        TouchManager.instance.OnSwipe -= SwipeAction;
    }
}

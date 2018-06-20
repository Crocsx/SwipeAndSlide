using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        TouchManager.instance.OnSwipe += SwipeAction;
    }

    void Start()
    {
        TouchManager.instance.OnSwipe += SwipeAction;
    }


    // Update is called once per frame
    void Update()
    {
    }

    void SwipeAction(TouchStruct touch, Vector2 direction)
    {
        Vector2 swipeDirection = ToolBox.instance.ClosestDirection(direction);
        Debug.Log(direction);
        Debug.Log(swipeDirection);
        NavigateStage((int)swipeDirection.x);
    }

    void NavigateStage(int value)
    {
        //StartCoroutine(SlideStages(value));
    }

    IEnumerator SlideStages(int value)
    {
        float time = 0;
        float slideSpeed = 0.5f;
        while (time < slideSpeed)
        {

            time += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        TouchManager.instance.OnSwipe -= SwipeAction;
    }
}

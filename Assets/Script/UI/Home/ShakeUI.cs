using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeUI : MonoBehaviour {

    public RectTransform[] uiElements;
    Dictionary<RectTransform, Vector3> uiElementsAndScale;

    void Start()
    {
        uiElementsAndScale = new Dictionary<RectTransform, Vector3>();
        for (var i = 0; i < uiElements.Length; i++)
            uiElementsAndScale.Add(uiElements[i], uiElements[i].localScale);
    }
    void Update()
    {
        foreach (KeyValuePair<RectTransform, Vector3> uiElementAndScale in uiElementsAndScale)
        {
            uiElementAndScale.Key.localScale = uiElementAndScale.Value;
            uiElementAndScale.Key.localScale = uiElementAndScale.Value + (uiElementAndScale.Key.localScale * AudioAnalyzer.instance.bassValue * 0.1f);
        }
    }
}

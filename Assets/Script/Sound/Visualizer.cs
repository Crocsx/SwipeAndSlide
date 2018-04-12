using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour {

    public int nbVisual = 10;
    public int distVisual = 1;
    public float scalerVisual = 50;
    public float smoothSpeedVisual = 500.0f;
    public float maxScaleVisual = 25.0f;
    public float discardPercentage = 0.5f;
    public AudioAnalyzer analyzer;

    private Transform[] visualList;
    private float[] visualScale;

    // Use this for initialization
    void Start () {
        SpawnLine();
    }

    void Update()
    {
        UpdateVisual();
    }

    private void SpawnLine()
    {
        visualScale = new float[nbVisual];
        visualList = new Transform[nbVisual];

        for(int i = 0; i < nbVisual; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            cube.transform.parent = transform;
            visualList[i] = cube.transform;
            visualList[i].position = Vector3.right * i * distVisual;
        }
    }

    void UpdateVisual()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)(analyzer.SAMPLE_SIZE * discardPercentage) / nbVisual ;

        while(visualIndex < nbVisual)
        {
            int j = 0;
            float sum = 0;
            while (j < averageSize)
            {
                sum += analyzer.spectrum[spectrumIndex];
                spectrumIndex++;
                j++;

            }

            float scaleY = sum / averageSize * scalerVisual;
            visualScale[visualIndex] -= TimeManager.instance.deltaTime * smoothSpeedVisual;

            if (visualScale[visualIndex] < scaleY)
                visualScale[visualIndex] = scaleY;

            if (visualScale[visualIndex] > maxScaleVisual)
                visualScale[visualIndex] = maxScaleVisual;
        
            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++;
        }
    }
}

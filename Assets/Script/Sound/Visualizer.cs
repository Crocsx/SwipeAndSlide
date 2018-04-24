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

    public Material material;

    // Use this for initialization
    void Start () {
        SpawnLine();
    }

    void Update()
    {
        UpdateVisual();
    }

    /// <summary>
    /// this is the function that spawn the line of cube
    /// if we want to make different shape for the visualizer, this is the function to modify
    /// 
    /// Set the size of the array according to the nb of visual
    /// for each visual spawn a cube at the given location
    /// </summary>
    private void SpawnLine()
    {
        visualScale = new float[nbVisual];
        visualList = new Transform[nbVisual];

        for(int i = 0; i < nbVisual; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            cube.GetComponent<Renderer>().material = material;
            cube.transform.parent = transform;
            visualList[i] = cube.transform;
            visualList[i].position = Vector3.right * i * distVisual;
        }
    }

    /// <summary>
    /// Update the visualizer, a bar of cube that get scaled
    /// 
    /// Calculate the number or "sample" to include per each cube
    /// loop trought each cube
    /// Get the average size of the sample represented by the cube
    /// reduce the Cube scale from precedent frame
    /// clamp the cube to max and min value
    /// Scale the cube
    /// </summary>
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

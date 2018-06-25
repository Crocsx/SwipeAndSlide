using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnalyzer : MonoBehaviour {

    public int SAMPLE_SIZE = 1024;
    public int dataChannel = 0;
    public float dbRefValue = 0.1f;

    float _rmsValue;
    public float rmsValue {get{return _rmsValue;}}
    float _dbValue;
    public float dbValue { get { return _dbValue; } }
    float _pitchValue;
    public float pitchValue { get { return _pitchValue; } }
    float _bassValue;
    public float bassValue { get { return _bassValue; } }
    float[] _spectrum;
    public float[] spectrum { get { return _spectrum; } }

    private AudioSource source;

    private float[] samples;
    private float sampleRate;

    public static AudioAnalyzer instance;
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        source = GetComponent<AudioSource>();
        samples = new float[SAMPLE_SIZE];
        _spectrum = new float[SAMPLE_SIZE];
    }

    void Start ()
    {
        sampleRate = AudioSettings.outputSampleRate;
    }
	
	void Update ()
    {
        AnalyzeSound();
    }

    void AnalyzeSound()
    {
        source.GetOutputData(samples, dataChannel);

        // Get the RMS value
        float sum = 0;
        for (int i = 0; i < SAMPLE_SIZE; i++)
        {
            sum += samples[i] * samples[i]; // sum squared samples
        }
        _rmsValue = Mathf.Sqrt(sum / SAMPLE_SIZE); // rms = square root of average

        // Get the DB value 

        _dbValue = 20 * Mathf.Log10(rmsValue / dbRefValue); // calculate dB                                

        // get sound spectrum
        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        // Get the Pitch value 
        float maxV = 0;
        float maxN = 0;
        for (int i = 0; i < SAMPLE_SIZE; i++)
        { // find max 
            if (spectrum[i] > maxV && spectrum[i] > 0.0f)
            {
                maxV = spectrum[i];
                maxN = i; // maxN is the index of max
            }
        }
        double freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < SAMPLE_SIZE - 1)
        { // interpolate index using neighbours
            float dL = spectrum[(int)(maxN - 1)] / spectrum[(int)maxN];
            float dR = spectrum[(int)(maxN + 1)] / spectrum[(int)maxN];
            freqN += 0.5 * (dR * dR - dL * dL);
        }

        _pitchValue = (float)freqN * (sampleRate / 2) / SAMPLE_SIZE; // convert index to frequency precision lost a bit from casting

        ///Not Accurate, Bass value
        // Basically calculate the first 7 sample in order to try to understand a somewhat of bass drop
        // _bassValue = 1 is considered a good bass drop
        int j = 0;
        float bassSum = 0;
        float BassSampleAverage = SAMPLE_SIZE * 0.007f;
        while (j < BassSampleAverage)
        {
            bassSum += spectrum[j];
            j++;
        }
        _bassValue = bassSum;
    }
}

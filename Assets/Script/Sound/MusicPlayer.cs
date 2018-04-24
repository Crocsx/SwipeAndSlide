using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public AudioClip music;
    public int defaultBPM;
    AudioSource source;

    public delegate void onBeat();
    public event onBeat OnBeat;

    private float currentBeat;
    private int currentBPM;
    private float timer;

    public static MusicPlayer instance;
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        currentBPM = defaultBPM;
        currentBeat = 60.0f / currentBPM;
    }
	
	void Update () {
        CheckBeat();
	}

    /// <summary>
    /// Check if it is time to call for the event Beat
    /// </summary>
    void CheckBeat()
    {
        timer += TimeManager.instance.deltaTime;
        if(timer > currentBeat)
        {
            if (OnBeat != null)
                OnBeat();
            timer = 0;
        }
    }

    /// <summary>
    /// Raise the BPM of the song so that the function Beat is called more
    /// </summary>
    /// <param name="factor">the factor to increase the beat </param>
    public void RaiseBPM(int factor)
    {
        currentBPM = currentBPM * factor;
        currentBeat = 60.0f / currentBPM;
    }

    /// <summary>
    /// lower the BPM of the song so that the function Beat is called more
    /// </summary>
    /// <param name="factor">the factor to lower the beat </param>
    public void LowerBPM(int factor)
    {
        currentBPM = currentBPM * factor;
        currentBeat = 60.0f / currentBPM;
    }
}

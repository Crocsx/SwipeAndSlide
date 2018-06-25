using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MusicPlayer : MonoBehaviour {

    public AudioClip musicC;
    public VideoClip videoC;
    public int defaultBPM;
    AudioSource mSource;
    VideoPlayer vSource;

    public delegate void onBeat();
    public event onBeat OnBeat;

    private float currentBeat;
    private int currentBPM;
    private float timer;

    private bool Started;

    public static MusicPlayer instance;
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        mSource = GetComponent<AudioSource>();
        vSource = GetComponent<VideoPlayer>();
        Started = false;
        currentBPM = defaultBPM;
        currentBeat = 60.0f / currentBPM;
    }

    void Start ()
    {
        SetClipSource();
    }
	
    /// <summary>
    /// Set the current clip, if video or music
    /// </summary>
    void SetClipSource()
    {
        if (musicC != null)
            mSource.clip = musicC;
        if (videoC != null)
        {
            vSource.clip = videoC;
            vSource.SetTargetAudioSource(0,mSource);
        }
    }
    

    void Update () {
        if(Started)
            CheckBeat();
	}

    public void Play()
    {
        Started = true;

        if (musicC != null)
            mSource.Play();
        if (videoC != null)
            vSource.Play();
    }

    public void Pause()
    {
        Started = false;

        if (musicC != null)
            mSource.Pause();
        if (videoC != null)
            vSource.Pause();
    }

    public void Stop()
    {
        Started = false;

        if (musicC != null)
            mSource.Stop();
        if (videoC != null)
            vSource.Stop();
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

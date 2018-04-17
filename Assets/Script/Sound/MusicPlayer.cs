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


    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        source.clip = music;
        source.Play();

        currentBPM = defaultBPM;
        currentBeat = 60.0f / currentBPM;
        Debug.Log(currentBPM);
    }
	
	// Update is called once per frame
	void Update () {
        CheckBeat();
	}

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

    void RaiseBPM(int factor)
    {
        currentBPM = currentBPM * factor;
        currentBeat = 60 / currentBPM;
    }

    void LowerBPM(int factor)
    {
        currentBPM = currentBPM * factor;
        currentBeat = 60 / currentBPM;
    }
}

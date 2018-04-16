using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public AudioClip music;
    public int BPM;
    AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.clip = music;
        source.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

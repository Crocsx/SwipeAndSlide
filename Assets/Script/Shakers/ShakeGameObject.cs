using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeGameObject : MonoBehaviour
{
    public float intensity = 3;
    Quaternion rot;

    private void Awake()
    {
        //MusicPlayer.instance.OnBeat += AskShake;
        rot = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = rot;
        Vector3 offset = Random.insideUnitSphere * AudioAnalyzer.instance.bassValue * intensity;
        transform.eulerAngles = new Vector3(rot.x + offset.x, rot.y + offset.y, 0);
    }
}
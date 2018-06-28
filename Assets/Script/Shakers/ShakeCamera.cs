using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float intensity = 5;
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

    /*
    void AskShake()
    {
        Shake(AudioAnalyzer.instance.rmsValue*50, 0.2f);
    }

    public void Shake(float intensity, float time)
    {
        //StartCoroutine(doShake(intensity, time));
    }

    /*IEnumerator doShake(float intensity, float time)
    {
        float timer = 0;
        Quaternion rot = transform.rotation;
        while (timer < time)
        {
            Vector3 offset = Random.insideUnitSphere * intensity;
            transform.eulerAngles = new Vector3(rot.x + offset.x, rot.y + offset.y, 0);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.rotation = rot;
    }

    private void OnDestroy()
    {
        MusicPlayer.instance.OnBeat -= AskShake;
    }*/
}
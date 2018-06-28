using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeParticle : MonoBehaviour
{
    public float intensity = 4;
    public ParticleSystem[] pSystem;
    Dictionary<ParticleSystem, float> pSystemsAndNoise;

    void Start()
    {
        pSystemsAndNoise = new Dictionary<ParticleSystem, float>();
        for (var i = 0; i < pSystem.Length; i++)
        {
            var noise = pSystem[i].noise;
            pSystemsAndNoise.Add(pSystem[i], noise.strength.constant);
        }
    }
    void Update()
    {
        foreach (KeyValuePair<ParticleSystem, float> pSystemAndNoise in pSystemsAndNoise)
        {
            var noise = pSystemAndNoise.Key.noise;
            noise.strength = pSystemAndNoise.Value + AudioAnalyzer.instance.bassValue * intensity;

        }
    }
}

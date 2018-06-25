using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRotator : MonoBehaviour {

    GridGenerator grid;
    int InversionStep = 8;
    int currentStep = 8;
    int side = 1;

    void Start ()
    {
        MusicPlayer.instance.OnBeat += Rotator;
        grid = GridManager.instance.GetGrid("movementGrid");
    }
	
    /// <summary>
    /// Rotate the grid with a random force
    /// </summary>
	void Rotator ()
    {
        currentStep--;
        if(currentStep <= 1)
        {
            currentStep = InversionStep;
            side = -side;
            GetComponent<Rigidbody2D>().AddTorque(side * Random.Range(2, 3) * (ScoreManager.instance.score/ 1000), ForceMode2D.Impulse);
        }
    }

    private void OnDestroy()
    {
        MusicPlayer.instance.OnBeat -= Rotator;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRotator : MonoBehaviour {

    GridGenerator grid;
    int InversionStep = 8;
    int currentStep = 8;
    int side = 1;

    void Start () {
        grid = GridManager.instance.GetGrid("movementGrid");
        MusicPlayer.instance.OnBeat += Rotator;
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
            GetComponent<Rigidbody2D>().AddTorque(side * Random.Range(2, 3), ForceMode2D.Impulse);
        }
    }
}

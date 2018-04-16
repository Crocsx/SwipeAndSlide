using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour {

    private SimpleGrid enemyGrid;
    void Start()
    {
        enemyGrid = GridManager.instance.GetGrid("enemyGrid");
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public static GridManager instance = null;

    public GameObject grid = null;

    public Dictionary<string, SimpleGrid> grids = new Dictionary<string, SimpleGrid>();
    void Awake()
    {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        SimpleGrid pGrid = Instantiate(grid, new Vector3(0, 0, 0), Quaternion.identity).transform.GetComponent<SimpleGrid>();
        pGrid.CreateGrid(Vector2.zero, 5, 5, 10, 10);
        grids.Add("movementGrid", pGrid);

        SimpleGrid eGrid = Instantiate(grid, new Vector3(0, 0, 0), Quaternion.identity).transform.GetComponent<SimpleGrid>();
        eGrid.CreateGrid(Vector2.zero, 7, 7, 10, 10);
        grids.Add("enemyGrid", eGrid);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

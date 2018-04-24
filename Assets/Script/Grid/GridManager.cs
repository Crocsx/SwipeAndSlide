using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public static GridManager instance = null;
    public GameObject container;
    public GameObject gridGenerator;

    public Dictionary<string, GridGenerator> grids = new Dictionary<string, GridGenerator>();
    void Awake()
    {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        SetupGrid();
    }

    // Use this for initialization
    void SetupGrid()
    {
        GridGenerator pGrid = Instantiate(gridGenerator, new Vector3(0, 0, 0), Quaternion.identity).transform.GetComponent<GridGenerator>();
        pGrid.CreateGrid("movementGrid", Vector2.zero, container, 5, 5, 10, 10);
        grids.Add("movementGrid", pGrid);

        GridGenerator eGrid = Instantiate(gridGenerator, new Vector3(0, 0, 0), Quaternion.identity).transform.GetComponent<GridGenerator>();
        eGrid.CreateGrid("enemyGrid", Vector2.zero, container, 7, 7, 10, 10);
        grids.Add("enemyGrid", eGrid);
    }

    // Update is called once per frame
    public GridGenerator GetGrid (string name)
    {
        if (grids.ContainsKey(name))
        {
            return grids[name];
        }
        else
        {
            Debug.LogError("No Grid Found");
            return null;
        }
	}
}

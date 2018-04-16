using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public static GridManager instance;

    public int GridSizeX = 5;
    public int GridSizeY = 5;
    public int GridSpaceX = 10;
    public int GridSpaceY = 10;

    private Vector2[,] _movementsGrid;
    private Vector2 _centerPosition = new Vector2();

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        SetupMovementGrid();
    }

    /// <summary>
    /// Setup a grid of possible movement (WORKS ONLY FOR GRID)
    /// 
    /// Set the distance beetween each X and Y point
    /// Set the current Player Position in the middle
    /// Calculate the minimum X and Y position reachable
    /// Iterate beetwen Xand Y in order to create each X and Y position on the grid
    /// 
    /// </summary>
    void SetupMovementGrid()
    {
        float XMin = Mathf.Floor(GridSizeX / 2) * GridSpaceX;
        float YMin = Mathf.Floor(GridSizeY / 2) * GridSpaceY;

        _centerPosition = new Vector2(Mathf.Round(GridSizeX / 2), Mathf.Round(GridSizeX / 2));

        float[] MovementX = new float[GridSizeX];
        float[] MovementY = new float[GridSizeY];

        Vector3 minPoint = new Vector2(transform.position.x - (XMin * _centerPosition.x), transform.position.y - (YMin * _centerPosition.y));

        _movementsGrid = new Vector2[GridSizeX, GridSizeY];

        for (int i = 0; i < GridSizeY; i++)
        {
            MovementY[i] = minPoint.y + (YMin + (i * GridSpaceY));
            for (int j = 0; j < GridSizeX; j++)
            {
                MovementX[j] = minPoint.y + (XMin + (j * GridSpaceX));
                _movementsGrid[i, j] = new Vector2(MovementY[i], MovementX[j]);

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(MovementY[i], MovementX[j], 0);
            }
        }
    }

    /// <summary>
    /// Get the position value for x and y value
    /// 
    /// <param name="x">index x of the position requested</param>
    /// <param name="y">index y of the position requested</param>
    /// <returns>return the position of the index inside the grid</returns>
    public Vector2 GetPosition(int x, int y)
    {
        Vector2 gridPosition = ClampIndexOnGrid(x, y);
        return _movementsGrid[(int)gridPosition.x, (int)gridPosition.y];
    }

    /// <summary>
    /// Get the center position of the grid
    /// 
    /// </summary>
    /// <returns>return the center position of the grid</returns>
    public Vector2 GetCenterPosition()
    {
        return _movementsGrid[GridSizeX/2, GridSizeY/2];
    }

    /// <summary>
    /// Get the center X and Y value of the grid
    /// 
    /// </summary>
    /// <returns>return the center index of the grid</returns>
    public Vector2 GetCenterIndex()
    {
        return new Vector2(Mathf.Floor(GridSizeX / 2), Mathf.Floor(GridSizeX / 2));
    }

    /// <summary>
    /// Clamp the value to get X and Y in the grid 
    /// 
    /// <param name="x">index x of the position requested</param>
    /// <param name="y">index y of the position requested</param>
    /// <returns>return the clamped index inside grid bounds</returns>
    public Vector2 ClampIndexOnGrid(int x, int y)
    {
        Vector2 clampedIndex = new Vector2(x > 0 ? x < GridSizeX ? x : GridSizeX - 1 : 0
                                        , y > 0 ? y < GridSizeY ? y : GridSizeY - 1 : 0);
        return clampedIndex;
    }
}

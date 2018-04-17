using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGrid : MonoBehaviour {

    public int gridSizeX { get { return _gridSizeX; } }
    int _gridSizeX;
    public int gridSizeY { get { return _gridSizeY; } }
    int _gridSizeY;
    public float gridSpaceX { get { return _gridSpaceX; } }
    float _gridSpaceX;
    public float gridSpaceY { get { return _gridSpaceY; } }
    float _gridSpaceY;
    public Vector2 centerPosition { get { return _centerPosition; } }
    Vector2 _centerPosition;
    public Vector2[][] grid { get { return _grid; } }
    private Vector2[][] _grid;

    public void CreateGrid(Vector2 center, int sizeX, int sizeY,float spaceX, float spaceY)
    {
        _centerPosition = center;
        _gridSizeX = sizeX;
        _gridSizeY = sizeY;
        _gridSpaceX = spaceX;
        _gridSpaceY = spaceY;
        SetupGrid();
    }

    /// <summary>
    /// Setup a grid of possible movement (WORKS ONLY FOR GRID)
    /// 
    /// Check Minimum distance of X and Y from center
    /// Calculate Minimum Point [0,0] of the grid compare to center
    /// Iterate beetwen X and Y in order to create each X and Y position on the grid
    /// 
    /// </summary>
    void SetupGrid()
    {
        float XMin = Mathf.Floor(_gridSizeX / 2) * _gridSpaceX;
        float YMin = Mathf.Floor(_gridSizeY / 2) * _gridSpaceY;
        Vector3 minPoint = new Vector2(centerPosition.x - XMin, centerPosition.y - YMin);

        float[] MovementX = new float[_gridSizeX];
        float[] MovementY = new float[_gridSizeY];

        _grid = new Vector2[_gridSizeY][];

        for (int i = 0; i < _gridSizeY; i++)
        {
            MovementY[i] = minPoint.y + (i * _gridSpaceY);
            _grid[i] = new Vector2[_gridSizeX];
            for (int j = 0; j < _gridSizeX; j++)
            {
                
                MovementX[j] = minPoint.x +(j * _gridSpaceX);
                _grid[i][j] = new Vector2(MovementY[i], MovementX[j]);

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.parent = transform;
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
        return _grid[(int)gridPosition.x][(int)gridPosition.y];
    }

    /// <summary>
    /// Get the center position of the grid
    /// 
    /// </summary>
    /// <returns>return the center position of the grid</returns>
    public Vector2 GetCenterPosition()
    {
        return _grid[_gridSizeX / 2][_gridSizeY / 2];
    }

    /// <summary>
    /// Get the center X and Y value of the grid
    /// 
    /// </summary>
    /// <returns>return the center index of the grid</returns>
    public Vector2 GetCenterIndex()
    {
        return new Vector2(Mathf.Floor(_gridSizeX / 2), Mathf.Floor(_gridSizeX / 2));
    }

    /// <summary>
    /// Clamp the value to get X and Y in the grid 
    /// 
    /// <param name="x">index x of the position requested</param>
    /// <param name="y">index y of the position requested</param>
    /// <returns>return the clamped index inside grid bounds</returns>
    public Vector2 ClampIndexOnGrid(int x, int y)
    {
        Vector2 clampedIndex = new Vector2(x > 0 ? x < _gridSizeX ? x : _gridSizeX - 1 : 0
                                        , y > 0 ? y < _gridSizeY ? y : _gridSizeY - 1 : 0);
        return clampedIndex;
    }
}

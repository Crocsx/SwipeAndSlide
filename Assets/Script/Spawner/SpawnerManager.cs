using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnerManager : MonoBehaviour {

    private SimpleGrid enemyGrid;

    Dictionary<Vector2, Vector2[]> spawnablePosition = new Dictionary<Vector2, Vector2[]>();

    void Start()
    {
        enemyGrid = GridManager.instance.GetGrid("enemyGrid");
        GetSpawnablePosition();
    }

    /// <summary>
    /// This is supposed to get the grid case where it is possible for the obstacle to spawn.
    /// see this image https://i.stack.imgur.com/qBJ1T.png
    /// first for loop will take value for the left and the right
    /// second loop will take the value for up and down
    /// </summary>
    void GetSpawnablePosition() {
        Vector2[] coordX = { Vector2.up, Vector2.down };
        Vector2[] coordY = { Vector2.left, Vector2.right };

        for (int i = 0; i < coordY.Length; i++)
        {
            Vector2[] newArray = new Vector2[enemyGrid.grid[0].Length - 2];

            if (coordY[i] == Vector2.left)
                System.Array.Copy(enemyGrid.grid[0], 1, newArray, 0, enemyGrid.grid[0].Length - 2);

            if (coordY[i] == Vector2.right)
                System.Array.Copy(enemyGrid.grid[enemyGrid.grid.Length - 1], 1, newArray, 0, enemyGrid.grid[enemyGrid.grid[0].Length - 1].Length - 2);
            spawnablePosition.Add(coordY[i], newArray);
        }
        
        for (int i = 0; i < coordY.Length; i++)
        {
            Vector2[] newArray = new Vector2[enemyGrid.grid.Length - 2];

            if (coordX[i] == Vector2.up)
            {
                for (int j = 0; j < enemyGrid.grid.Length - 2; j++)
                {
                    newArray[j] = enemyGrid.grid[j+1][0];
                }
            }
            if (coordX[i] == Vector2.down)
            {
                for (int j = 0; j < enemyGrid.grid.Length - 2; j++)
                {
                    newArray[j] = enemyGrid.grid[j + 1][enemyGrid.grid[0].Length-1];
                }
            }

            spawnablePosition.Add(coordX[i], newArray);
        }
    }
}

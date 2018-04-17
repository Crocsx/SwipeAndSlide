using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour {

    private SimpleGrid enemyGrid;
    public GameObject enemy;
    Dictionary<Vector2, Vector2[]> spawnablePosition = new Dictionary<Vector2, Vector2[]>();

    void Start()
    {
        enemyGrid = GridManager.instance.GetGrid("enemyGrid");
        MusicPlayer.instance.OnBeat += Spawn;
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
            {
                for (int j = 0; j < enemyGrid.grid[0].Length - 2; j++)
                {
                    newArray[j] = new Vector2(0, j+1);
                }
            }

            if (coordY[i] == Vector2.right)
            {
                for (int j = 0; j < enemyGrid.grid[0].Length - 2; j++)
                {
                    newArray[j] = new Vector2(enemyGrid.grid[0].Length - 1, j + 1);
                }
            }
            spawnablePosition.Add(coordY[i], newArray);
        }
        
        for (int i = 0; i < coordY.Length; i++)
        {
            Vector2[] newArray = new Vector2[enemyGrid.grid.Length - 1];

            if (coordX[i] == Vector2.down)
            {
                for (int j = 0; j <= enemyGrid.grid.Length - 2; j++)
                {
                    newArray[j] = new Vector2(j+1,0);
                }
            }
            if (coordX[i] == Vector2.up)
            {
                for (int j = 0; j <= enemyGrid.grid.Length - 2; j++)
                {
                    newArray[j] = new Vector2(j + 1, enemyGrid.grid[0].Length - 1);
                }
            }

            spawnablePosition.Add(coordX[i], newArray);
        }
    }

    private void Update()
    {
        Camera.main.backgroundColor = Color.blue;
    }

    private void Spawn()
    { 
        Vector2 randKey = spawnablePosition.Keys.ElementAt((int)Random.Range(0, spawnablePosition.Keys.Count));
        int randValue = Random.Range(0, spawnablePosition[Vector2.up].Length - 1);
        Vector2 pos = enemyGrid.GetPosition(spawnablePosition[randKey][randValue].x, spawnablePosition[randKey][randValue].y);

        Enemy script = GameObject.Instantiate(enemy, pos, Quaternion.identity).GetComponent<Enemy>();

        script.transform.up = randKey;
        script.Setup(randKey, spawnablePosition[randKey][randValue], enemyGrid);

        Camera.main.backgroundColor = Color.red;
    }

    private void OnDestroy()
    {
        MusicPlayer.instance.OnBeat -= Spawn;
    }
}

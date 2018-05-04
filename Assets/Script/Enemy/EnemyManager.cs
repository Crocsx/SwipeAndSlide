using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour {

    private GridGenerator enemyGrid;
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
        var yLength = enemyGrid.grid[0].Length;
        var xLength = enemyGrid.grid.Length;

        spawnablePosition.Add(Vector2.left, Enumerable.Range(1, yLength).Select(y => new Vector2(0, y)).ToArray());
        spawnablePosition.Add(Vector2.right, Enumerable.Range(1, yLength).Select(y => new Vector2(xLength - 1, y)).ToArray());
        spawnablePosition.Add(Vector2.down, Enumerable.Range(1, xLength).Select(x => new Vector2(x, 0)).ToArray());
        spawnablePosition.Add(Vector2.up, Enumerable.Range(1, xLength).Select(x => new Vector2(x, yLength - 1)).ToArray());
    }

    /// <summary>
    /// Get a randomKey inside the spawnablePosition
    /// get a random value inside spawnablePosition
    /// get the position of the random location of the grid
    /// 
    /// Spawn a new Enemy inside the grid
    /// 
    /// Setup the new Enemy
    /// </summary>
    private void Spawn()
    {
        Vector2 randKey = spawnablePosition.Keys.ElementAt((int)Random.Range(0, spawnablePosition.Keys.Count));
        int randValue = Random.Range(0, spawnablePosition[Vector2.up].Length - 1);
        Vector2 pos = enemyGrid.GetPosition(spawnablePosition[randKey][randValue].x, spawnablePosition[randKey][randValue].y);

        Enemy newEnemy = enemyGrid.SpawnOnGrid(enemy, pos, Quaternion.identity).GetComponent<Enemy>();

        newEnemy.transform.up = (Vector3)randKey;

        float z = enemyGrid.container.transform.rotation.eulerAngles.z;

        // fix the fact that it doesn't rotate correctly if down
        int multi = 1;
        if (randKey == Vector2.down)
            multi = -1;

        newEnemy.transform.eulerAngles = newEnemy.transform.eulerAngles + multi * new Vector3(0, 0, z);

        newEnemy.Setup(randKey, spawnablePosition[randKey][randValue], enemyGrid);
    }

    private void OnDestroy()
    {
        MusicPlayer.instance.OnBeat -= Spawn;
    }
}

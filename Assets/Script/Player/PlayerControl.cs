using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    // in second
    public float MovementSpeed = 0.1f;
    public float MovementDistance = 10;
    public int MovementXAvailable = 5;
    public int MovementYAvailable = 5;

    private Vector2 currentPosition = new Vector2();
    private Vector2[,] MovementsGrid;

    void Start () {
        TouchManager.instance.OnSwipe += Movement;
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
        float XMin = Mathf.Floor(MovementXAvailable / 2) * MovementDistance;
        float YMin = Mathf.Floor(MovementYAvailable / 2) * MovementDistance;

        currentPosition = new Vector2(Mathf.Round(MovementXAvailable / 2), Mathf.Round(MovementXAvailable / 2));

        float[] MovementX = new float[MovementXAvailable];
        float[] MovementY = new float[MovementYAvailable];

        Vector3 minPoint = new Vector2(transform.position.x - (XMin * currentPosition.x), transform.position.y - (YMin * currentPosition.y));

        MovementsGrid = new Vector2[MovementXAvailable, MovementYAvailable];

        for (int i = 0; i < MovementYAvailable; i++)
        {
            MovementY[i] = minPoint.y + (YMin + (i * MovementDistance));
            for (int j = 0; j < MovementXAvailable; j++)
            {
                MovementX[j] = minPoint.y + (XMin + (j * MovementDistance));
                MovementsGrid[i, j] = new Vector2(MovementY[i], MovementX[j]);

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(MovementY[i], MovementX[j], 0);
            }
        }

    }

    /// <summary>
    /// Do the calculation for the next player movement
    /// 
    /// Calculate the swipe direction
    /// Calculate the next position if we move
    /// Check if the move is not out of bound
    /// Apply the movement
    /// </summary>
    /// <param name="touch"></param>
    /// <param name="direction"></param>
    void Movement (TouchStruct touch, Vector2 direction) {
        Vector3 swipeDirection = ToolBox.instance.ClosestDirection(direction);

        float nextPosX = currentPosition.x - swipeDirection.x;
        float nextPosY = currentPosition.y - swipeDirection.y;
        currentPosition = new Vector2(nextPosX > 0 ? nextPosX < MovementXAvailable ? nextPosX : MovementXAvailable - 1 : 0
                                        , nextPosY > 0 ? nextPosY < MovementYAvailable ? nextPosY : MovementYAvailable - 1 : 0);

        Vector2 targetPos = MovementsGrid[(int)currentPosition.x, (int)currentPosition.y];
        StartCoroutine(MoveAnimation(targetPos));
    }

    /// <summary>
    /// Move the player in the given direction
    /// 
    /// Set the starting point, and target position
    /// Move in the while at the givent speed
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    IEnumerator MoveAnimation(Vector2 targetPos)
    {
        Vector2 startPos = transform.position;
        float timer = 0;
        while (timer < MovementSpeed)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, (timer / MovementSpeed));
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }

    private void OnDestroy()
    {
        TouchManager.instance.OnSwipe -= Movement;
    }
}

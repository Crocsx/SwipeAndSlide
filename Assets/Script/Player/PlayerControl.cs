using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    // in second
    public float MovementSpeed = 0.1f;
    public int MovementXAvailable = 5;
    public int MovementYAvailable = 5;

    private Vector2 currentGridIndex = new Vector2();
    private Vector2 targetGridIndex = new Vector2();
    private SimpleGrid movementGrid;

    void Start ()
    {
        TouchManager.instance.OnSwipe += Movement;
        movementGrid = GridManager.instance.GetGrid("movementGrid");
        currentGridIndex = movementGrid.GetCenterIndex();
        transform.position = movementGrid.GetCenterPosition();
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
    void Movement(TouchStruct touch, Vector2 direction)
    {
        Vector3 swipeDirection = ToolBox.instance.ClosestDirection(direction);

        float nextPosX = currentGridIndex.x - swipeDirection.x;
        float nextPosY = currentGridIndex.y - swipeDirection.y;

        targetGridIndex = movementGrid.ClampIndexOnGrid((int)nextPosX, (int)nextPosY);
        StartCoroutine(MoveAnimation(movementGrid.GetPosition((int)targetGridIndex.x, (int)targetGridIndex.y)));
    }

    /// <summary>
    /// Move the player in the given direction
    /// 
    /// Set the starting point, and target position
    /// Move in the while at the givent speed
    /// Set the target position to current position
    /// </summary>
    /// <param name="targetPos"></param>
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
        currentGridIndex = targetGridIndex;
        transform.position = targetPos;
    }

    private void OnDestroy()
    {
        TouchManager.instance.OnSwipe -= Movement;
    }
}

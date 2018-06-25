using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    // in second
    public float MovementSpeed = 0.1f;
    public int MovementXAvailable = 5;
    public int MovementYAvailable = 5;

    Transform body;
    TriangleCreator triangleCreator;

    private Vector2 currentGridIndex = new Vector2();
    private Vector2 targetGridIndex = new Vector2();
    private GridGenerator movementGrid;

    private Vector2 lastDirection = Vector2.down;

    private void OnEnable()
    {
        TouchManager.instance.OnSwipe += Movement;
    }

    private void OnDisable()
    {
        TouchManager.instance.OnSwipe -= Movement;
    }

    void Start ()
    {
        body = transform.GetChild(0);
        triangleCreator = body.GetComponent<TriangleCreator>();

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
        Vector3 faceDir = ToolBox.instance.ClosestDirection(-body.up);

        float angle = ToolBox.instance.ClosestAngle2D(-swipeDirection, lastDirection);
        body.Rotate(new Vector3(0, 0, angle), Space.Self);

        float nextPosX = currentGridIndex.x + swipeDirection.x;
        float nextPosY = currentGridIndex.y + swipeDirection.y;

        lastDirection = -swipeDirection;

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
        triangleCreator.TransformTriangle(MovementSpeed/2);
        while (timer < MovementSpeed)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, (timer / MovementSpeed));
            timer += Time.deltaTime;
            yield return null;
        }
        triangleCreator.TransformSquare(MovementSpeed / 2);
        currentGridIndex = targetGridIndex;
        transform.position = movementGrid.GetPosition((int)targetGridIndex.x, (int)targetGridIndex.y);
    }

    private void OnDestroy()
    {
        TouchManager.instance.OnSwipe -= Movement;
    }
}

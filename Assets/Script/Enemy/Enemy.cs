using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float MovementSpeed = 0.1f;

    Vector2 direction;
    Vector2 currentGridIndex;
    Vector2 targetGridIndex;
    SimpleGrid movementGrid;

    // Use this for initialization
    void Start ()
    {
        MusicPlayer.instance.OnBeat += Move;
    }
	
    public void Setup(Vector2 dir, Vector2 spawnPos, SimpleGrid grid)
    {
        direction = dir;
        currentGridIndex = spawnPos;
        movementGrid = grid;
    }


	// Update is called once per frame
	void Move()
    {
        float nextPosX = currentGridIndex.x - direction.x;
        float nextPosY = currentGridIndex.y - direction.y;

        if (movementGrid.IndexExist((int)nextPosX, (int)nextPosY)){
            targetGridIndex = movementGrid.ClampIndexOnGrid((int)nextPosX, (int)nextPosY);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }

        StartCoroutine(MoveAnimation(movementGrid.GetPosition(targetGridIndex.x, targetGridIndex.y)));
    }

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
        MusicPlayer.instance.OnBeat -= Move;
    }
}

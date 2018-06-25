using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float MovementSpeed = 0.1f;
    public TriangleCreator triangleCreator;

    public GameObject effectExplosion;

    Vector2 direction;
    Vector2 currentGridIndex;
    Vector2 targetGridIndex;
    GridGenerator movementGrid;
    bool hasEntered = false;

    EnemyManager eManager;

    void OnEnable ()
    {
        MusicPlayer.instance.OnBeat += NextMove;
    }

    void OnDisable()
    {
        MusicPlayer.instance.OnBeat -= NextMove;
    }

    public void Setup(EnemyManager manager, Vector2 dir, Vector2 spawnPos, GridGenerator grid)
    {
        eManager = manager;
        direction = dir;
        currentGridIndex = spawnPos;
        movementGrid = grid;
    }

    /// <summary>
    /// If it s the first move play triangle animation.
    /// Else search possible position on the grid
    ///  and get killed if no more movement are available
    /// </summary>
    void NextMove()
    {
        if (!hasEntered)
        {
            triangleCreator.TransformTriangle(MovementSpeed);
            hasEntered = true;
            return;
        }

        float nextPosX = currentGridIndex.x - direction.x;
        float nextPosY = currentGridIndex.y - direction.y;

        if (movementGrid.IndexExist((int)nextPosX, (int)nextPosY)){
            targetGridIndex = movementGrid.ClampIndexOnGrid((int)nextPosX, (int)nextPosY);
        }
        else
        {
            eManager.Killed(this);
            GameObject.Destroy(gameObject);
        }

        StartCoroutine(MoveAnimation(movementGrid.GetPosition(targetGridIndex.x, targetGridIndex.y)));
    }

    /// <summary>
    /// Move according to the time
    /// </summary>
    IEnumerator MoveAnimation(Vector2 targetPos)
    {
        Vector2 startPos = transform.position;
        float timer = 0;
        GetComponent<ParticleSystem>().Play();
        while (timer < MovementSpeed)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, (timer / MovementSpeed));
            timer += Time.deltaTime;
            yield return null;
        }
        currentGridIndex = targetGridIndex;
        transform.position = movementGrid.GetPosition(targetGridIndex.x, targetGridIndex.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Player>().TakeDamage();
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(effectExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        MusicPlayer.instance.OnBeat -= NextMove;
    }
}

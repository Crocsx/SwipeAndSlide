using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCreator : MonoBehaviour {

    public float width = 4;
    public float height = 4;
    public Material material;

    Mesh m_meshTriangle;

    /// <summary>
    /// Create a square formed by 4 triangle
    /// Check this http://prntscr.com/jdj7sg
    /// </summary>
	void Start () {
        m_meshTriangle = GetComponent<MeshFilter>().mesh;
        MeshRenderer m_meshRendererTriangle = GetComponent<MeshRenderer>();
        m_meshTriangle.Clear();

        m_meshRendererTriangle.sortingLayerName = "GameElement";
        m_meshRendererTriangle.sortingOrder = 0;

        m_meshTriangle.vertices = new Vector3[] 
        {
            new Vector3(-width/2, -height/2, 0), new Vector3(0, -height/2, 0), new Vector3(width/2, -height/2, 0),
            new Vector3(-width/2, height/2, 0), new Vector3(0, height/2, 0), new Vector3(width/2, height/2, 0)
        };
        
        m_meshTriangle.triangles = new int[] {
            0, 3, 4,
            1, 0, 4,
            2, 1, 4,
            5, 2, 4,
        };

        m_meshTriangle.normals = new Vector3[]
        {
            Vector3.forward, Vector3.forward, Vector3.forward,
            Vector3.forward, Vector3.forward, Vector3.forward
        };

        m_meshTriangle.uv = new Vector2[] {
            new Vector2(0, 0), new Vector2(0.5f, 0f), new Vector2(1f, 0f),
            new Vector2(0, 1f), new Vector2(0.5f, 1f), new Vector2(1f, 1f)
        };

        m_meshRendererTriangle.material = material;
    }

    public void TransformTriangle(float time)
    {
        StartCoroutine(TransformInTriangle(time));
    }

    public void TransformSquare(float time)
    {
        StartCoroutine(TransformInSquare(time));
    }

    /// <summary>
    /// Coroutine that make the square become a triangle by moving the correct vertices
    /// </summary>
    /// <returns></returns>
    IEnumerator TransformInTriangle(float time)
    {
        Vector2 startPos = transform.position;
        float timer = 0;

        Vector3[] vertices = m_meshTriangle.vertices;

        Vector3 initv3 = vertices[3];
        Vector3 initv5 = vertices[5];
        while (timer < time)
        {
            vertices[3] = Vector2.Lerp(initv3, vertices[0], (timer / 2));
            vertices[5] = Vector2.Lerp(initv5, vertices[2], (timer / 2));
            m_meshTriangle.vertices = vertices;
            timer += Time.deltaTime;
            yield return null;
        }

        vertices[3] = vertices[0];
        vertices[5] = vertices[2];
        m_meshTriangle.vertices = vertices;
    }

    /// <summary>
    /// Coroutine that make the triangle become a square by moving the correct vertices
    /// </summary>
    /// <returns></returns>
    IEnumerator TransformInSquare(float time)
    {
        Vector2 startPos = transform.position;
        float timer = 0;

        Vector3[] vertices = m_meshTriangle.vertices;

        Vector3 initv3 = vertices[3];
        Vector3 initv5 = vertices[5];
        while (timer < time)
        {
            vertices[3] = Vector2.Lerp(initv3, new Vector2(-width / 2, height / 2), (timer / 2));
            vertices[5] = Vector2.Lerp(initv5, new Vector2(width / 2, height / 2), (timer / 2));
            m_meshTriangle.vertices = vertices;
            timer += Time.deltaTime;
            yield return null;
        }

        vertices[3] = new Vector2(-width / 2, height / 2);
        vertices[5] = new Vector2(width / 2, height / 2);
        m_meshTriangle.vertices = vertices;
    }
}

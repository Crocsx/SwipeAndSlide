using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : MonoBehaviour {

    public static ToolBox instance = null;

    #region Singleton Initialization 
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(transform.gameObject);
    }
    #endregion Singleton Initialization 


    private Vector3[] compass = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, Vector3.back, Vector3.up, Vector3.down };
    public Vector3 ClosestDirection(Vector3 vector)
    {
        float maxDot = -Mathf.Infinity;
        Vector3 ret = Vector3.zero;
     
        foreach(Vector3 dir in compass)
        { 
            float t = Vector3.Dot(vector, dir);
            if (t > maxDot)
            {
                ret = dir;
                maxDot = t;
            }
        }
        return ret;
    }

    public float ClosestAngle2D(Vector2 swipeDir, Vector2 lastDir)
    {
        float ang = Vector3.Angle(swipeDir, lastDir);

        if(lastDir == Vector2.up)
        {
            if (swipeDir == Vector2.right)
                ang = -ang;
        }

        if (lastDir == Vector2.right)
        {
            if (swipeDir == Vector2.down)
                ang = -ang;
        }

        if (lastDir == Vector2.left)
        {
            if (swipeDir == Vector2.up)
                ang = -ang;
        }

        if (lastDir == Vector2.down)
        {
            if (swipeDir == Vector2.left)
                ang = -ang;
        }
        return ang;
    }
}

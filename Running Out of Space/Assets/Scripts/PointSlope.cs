using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSlope : MonoBehaviour
{

    public Transform point1;
    public Transform point2;
    public Transform cursor;
    public static int contains;
    public int c;
    public float slope;


    void Start()
    {
    }

    void Update()
    {
        slope = (point1.position.y - point2.position.y) / (point1.position.x - point2.position.x);

        if (cursor.position.y > slope * cursor.position.x - slope * point1.position.x + point1.position.y)
        {
            contains = 1;
        }
        else
        {
            contains = -1;
        }
        c = contains;

    }
}

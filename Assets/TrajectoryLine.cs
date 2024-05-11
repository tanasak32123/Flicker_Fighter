using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    public LineRenderer lr;
    // Start is called before the first frame update

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;
        var force = new Vector2(startPoint.x - endPoint.x, startPoint.y - endPoint.y);
        if (force.sqrMagnitude < 5 || force.sqrMagnitude > 100)
        {
            lr.startColor = Color.red; lr.endColor = Color.red;
        }
        else
        {
            lr.startColor = Color.white; lr.endColor = Color.white;
        }
        lr.SetPositions(points);
    }

    public void RemoveLine()
    {
        lr.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
    }

    public void EndLine()
    {
        lr.positionCount = 0;
    }
}

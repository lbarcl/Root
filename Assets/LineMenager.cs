using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMenager : MonoBehaviour
{
    private LineRenderer line;
    private List<Vector3> points;
    public Vector3 lastPoint;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        points = new List<Vector3>();
    }

    public void AddPoint(Vector3 point)
    {
        lastPoint = point;
        points.Add(point);
        DrawLine();
    }

    public int GetIndex(Vector3 vector)
    {
        return points.IndexOf(vector);
    }

    void DrawLine()
    {
        line.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            line.SetPosition(i, points[i]);
        }
    }

    public void RemoveFromEnd(int until = 0)
    {
        StartCoroutine(RemoveEnd(until));
    }

    IEnumerator RemoveEnd(int until = 0)
    {
        if (points.Count - 1 != until)
        {
            points.RemoveAt(points.Count - 1);
            DrawLine();
            lastPoint = points[points.Count - 1];
            yield return new WaitForSeconds(.05f);
            StartCoroutine(RemoveEnd(until));
        }
    }
}

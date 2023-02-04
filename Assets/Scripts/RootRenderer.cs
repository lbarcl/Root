using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootRenderer : MonoBehaviour
{
    [SerializeField] List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    Mesh rootMesh;
    int count;

    void Start()
    {
        rootMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = rootMesh;

    }

    private void UpdateMesh()
    {
        rootMesh.Clear();
        rootMesh.SetVertices(vertices);
        rootMesh.triangles = triangles.ToArray();
        rootMesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = rootMesh;
    }

    public void AddPoint(Vector3 point, float r = .4f)
    {
        int tmp = vertices.Count;
        vertices.Add(point); // 0
        vertices.Add(point + new Vector3(r, 0)); // 2
        vertices.Add(point + new Vector3(0, r)); // 1

        triangles.Add(1);
        triangles.Add(0);
        triangles.Add(2);

        count++;
        if (count < 2) return;

        triangles.Add(tmp - 3);
        triangles.Add(tmp - 2);
        triangles.Add(tmp + 1);
        
        triangles.Add(tmp - 3);
        triangles.Add(tmp + 1);
        triangles.Add(tmp);

        triangles.Add(tmp + 2);
        triangles.Add(tmp - 2);
        triangles.Add(tmp - 1);

        triangles.Add(tmp - 2);
        triangles.Add(tmp + 2);
        triangles.Add(tmp + 1);

        triangles.Add(tmp + 2);
        triangles.Add(tmp - 1);
        triangles.Add(tmp - 3);

        triangles.Add(tmp - 3);
        triangles.Add(tmp);
        triangles.Add(tmp + 2);

        UpdateMesh();
    }
}

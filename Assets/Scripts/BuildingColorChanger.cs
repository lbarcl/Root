using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingColorChanger : MonoBehaviour
{
    [SerializeField] MeshRenderer[] aptMeshRender;

    void Start()
    {
        for (int i = 0; i < aptMeshRender.Length; i++)
        {
            aptMeshRender[i].materials[0].color = RandomColor();
        }        
    }

    Color RandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }
}

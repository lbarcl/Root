using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Platforms; // Platform#0 will be starting platform and last platfrom will be endindg platform
    [SerializeField] int PlatformCount;
    [SerializeField] float PlatformLength;

    private void Start()
    {
        Vector3 lastPlatformPosition = transform.position;
        Instantiate(Platforms[0], lastPlatformPosition, Quaternion.identity);
        for (int i = 0; i < PlatformCount; i++)
        {
            lastPlatformPosition += new Vector3(0, 0, PlatformLength);
            Instantiate(Platforms[Random.Range(1, Platforms.Length - 1)], lastPlatformPosition, Quaternion.identity);
        }
        lastPlatformPosition += new Vector3(0, 0, PlatformLength);
        Instantiate(Platforms[Platforms.Length - 1], lastPlatformPosition, Quaternion.identity);
    }
}

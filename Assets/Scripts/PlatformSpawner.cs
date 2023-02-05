using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] upPlatforms; // Platform#0 will be starting platform and last platfrom will be endindg platform
    [SerializeField] GameObject[] downPlatforms; // Platform#0 will be starting platform and last platfrom will be endindg platform
    [SerializeField] int platformCount;
    [SerializeField] float platformLength;

    private void Start()
    {
        Vector3 lastPlatformPosition = transform.position;
        Instantiate(upPlatforms[0], lastPlatformPosition + new Vector3(1, 8, 0), Quaternion.identity);
        Instantiate(downPlatforms[0], lastPlatformPosition, Quaternion.Euler(0,-90,0));

        for (int i = 0; i < platformCount; i++)
        {
            lastPlatformPosition += new Vector3(0, 0, platformLength);
            Instantiate(upPlatforms[Random.Range(1, upPlatforms.Length - 1)], lastPlatformPosition + new Vector3(1,8,0), Quaternion.identity);
            Instantiate(downPlatforms[Random.Range(1, downPlatforms.Length - 1)], lastPlatformPosition, Quaternion.Euler(0,-90,0));

        }
        lastPlatformPosition += new Vector3(0, 0, platformLength);
        Instantiate(upPlatforms[upPlatforms.Length - 1], lastPlatformPosition + new Vector3(1, 8, 0), Quaternion.identity);
        Instantiate(downPlatforms[downPlatforms.Length - 1], lastPlatformPosition, Quaternion.Euler(0,-90,0));
    }
}

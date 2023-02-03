using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] RootRenderer root;
    float MaxDistance = 30f;
    Vector3 lastPoinPosition;
    bool stop = false;

    private void Start()
    {
        root.AddPoint(transform.position);
        lastPoinPosition = transform.position;
    }

    void Update()
    {
        if (MaxDistance <= 0) return;

        transform.position += new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), .5f) * speed;
        if (Vector3.Distance(lastPoinPosition, transform.position) > .5f)
        {
            root.AddPoint(transform.position, MaxDistance / 30);
            lastPoinPosition = transform.position;
            MaxDistance -= 1;
        }
    }
}

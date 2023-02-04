using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] float Speed;
    float CurrentDistance, MaxDistance = 100;
    
    [SerializeField] LineMenager lm;
    Rigidbody rb;

    Vector3 LastPointPosition;
    Vector3 CheckPointPosition;
    int CheckPointIndex;
    bool Stop = false, remove = false;

    private void Start()
    {
        LastPointPosition = transform.position;
        lm.AddPoint(transform.position);
        rb = GetComponent<Rigidbody>();
        CurrentDistance = MaxDistance;
    }

    void Update()
    {
        
        if (CurrentDistance <= 0 || Stop)
        {
            if (CheckPointPosition != Vector3.zero) Go2CheckPoint();
            else
            {
                Stop = true;
                Debug.Log("Game Over");
            }

        } else if (!Stop) Move();
    }

    void Go2CheckPoint()
    {
        if (CheckPointPosition == transform.position) 
        {
            Stop = false;
            remove = false;
            CurrentDistance = MaxDistance;
        }
        else
        {
            if (!remove)
            {
                remove = true;
                if (CheckPointIndex > 0)
                    lm.RemoveFromEnd(CheckPointIndex);
            }
            transform.position = lm.lastPoint;
        }
    }

    void Move()
    {
        rb.position += new Vector3(0, Input.GetAxis("Vertical"), .5f) * Speed;
        if (Vector3.Distance(LastPointPosition, transform.position) > .05f)
        {
            lm.AddPoint(transform.position);
            LastPointPosition = transform.position;
            CurrentDistance -= 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Concrete"))
        {
            Stop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            CheckPointPosition = transform.position;
            lm.AddPoint(transform.position);
            CheckPointIndex = lm.GetIndex(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            CurrentDistance += 50;
            MaxDistance += 50;
            other.enabled = false;
        }
    }
}

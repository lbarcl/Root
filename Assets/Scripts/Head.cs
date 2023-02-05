using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] float Speed, CurrentSpeed;
    float CurrentDistance, MaxDistance = 400;
    
    [SerializeField] LineMenager lm;
    [SerializeField] GameObject SpeedTrail;
    Rigidbody rb;

    Vector3 LastPointPosition;
    Vector3 CheckPointPosition;
    int CheckPointIndex;
    bool Stop = false, remove = false, finishGame = false;

    private void Start()
    {
        CurrentDistance = MaxDistance;
        CurrentSpeed = Speed;
        LastPointPosition = transform.position;
        lm.AddPoint(transform.position);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        if (CurrentDistance <= 0 || Stop && !finishGame)
        {
            if (CurrentSpeed == Speed * 4)
                CurrentSpeed = Speed / 4; 
            SpeedTrail.SetActive(false);
            if (CheckPointPosition != Vector3.zero) Go2CheckPoint();
            else
            {
                Stop = true;
                Debug.Log("Game Over");
            }

        } else if (!Stop) Move();
        Debug.Log("Current: " + CurrentDistance);
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CurrentSpeed = Speed * 4;
            SpeedTrail.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) { 
            CurrentSpeed = Speed / 4; 
            SpeedTrail.SetActive(false);
        }

        rb.position += new Vector3(0, Input.GetAxis("Vertical"), .5f) * CurrentSpeed;
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
        } else if (collision.collider.CompareTag("Finish"))
        {
            Debug.Log("You won");
            finishGame = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            if (Vector3.Distance(CheckPointPosition, transform.position) > 5f)
            {
                CheckPointPosition = transform.position;
                lm.AddPoint(transform.position);
                CheckPointIndex = lm.GetIndex(transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            CurrentDistance += 50;
            MaxDistance += 50;
            Destroy(other.gameObject);
        }
    }
}

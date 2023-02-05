using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class Head : MonoBehaviour
{
    [SerializeField] float Speed, CurrentSpeed;
    float CurrentDistance, MaxDistance = 400;
    
    [SerializeField] LineMenager lm;
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource audioSourceOfDrill;
    [SerializeField] AudioSource audioSourceOfSFX;
    [SerializeField] GameObject SpeedTrail;
    [SerializeField] GameObject litteTree;
    [SerializeField] Slider waterLevel;
    GameMenager menager;
    Rigidbody rb;

    Vector3 LastPointPosition;
    Vector3 CheckPointPosition;
    int CheckPointIndex;
    bool Stop = false, remove = false, finishGame = false;

    private void Start()
    {
        CurrentDistance = MaxDistance;
        waterLevel.maxValue = MaxDistance;
        CurrentSpeed = Speed;
        LastPointPosition = transform.position;
        lm.AddPoint(transform.position);
        rb = GetComponent<Rigidbody>();
        menager = GameObject.FindGameObjectWithTag("GameMenager").GetComponent<GameMenager>();
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
                finishGame = true;
                menager.LoadBadEnd();
            }

        } else if (!Stop) Move();
        waterLevel.value = CurrentDistance;
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
                {
                    lm.RemoveFromEnd(CheckPointIndex);
                    play(4);
                }
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
            audioSourceOfDrill.clip = clips[0];
            audioSourceOfDrill.Play();
        }
    }

    void play(int index)
    {
        audioSourceOfSFX.clip = clips[index];
        audioSourceOfSFX.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Concrete"))
        {
            Stop = true;
            play(3);
        } else if (collision.collider.CompareTag("Finish"))
        {
            menager.LoadGoodEnd();
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
                Instantiate(litteTree, LastPointPosition, Quaternion.identity);
                play(1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            CurrentDistance += 100;
            MaxDistance += 100;
            waterLevel.maxValue = MaxDistance;
            Destroy(other.gameObject);
            play(2);
        }
    }
}

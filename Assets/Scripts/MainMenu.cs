using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    GameMenager menager;

    private void Start()
    {
        menager = GameObject.FindGameObjectWithTag("GameMenager").GetComponent<GameMenager>();
    }

    public void Play()
    {
        menager.LoadLevel(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

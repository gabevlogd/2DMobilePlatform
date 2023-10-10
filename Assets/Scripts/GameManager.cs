using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static EventManager<Enumerators.Events, string> EventManager;

    protected override void Awake()
    {
        base.Awake();
        EventManager = new EventManager<Enumerators.Events, string>();
        EventManager.Register(Enumerators.Events.GameOver, OnGameOver);
    }

    public void OnGameOver(string str)
    {
        if (str == "Win")
            SceneManager.LoadScene("WinScene");
        else
        {
            PlayerStats.Attempts--;
            SceneManager.LoadScene("LoseScene");
        }
    }
}

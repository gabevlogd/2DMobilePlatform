using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private HeartsBar heartsBar;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI attempts;

    private void Awake()
    {
        attempts.text = "x" + PlayerStats.Attempts;
        GameManager.EventManager.Register(Enumerators.Events.LifeChange, OnLifeChange);
        GameManager.EventManager.Register(Enumerators.Events.ScoreChange, OnScoreChange);
    }

    

    public void OnLifeChange(string lifeToAddStr)
    {
        int life = heartsBar.GetLife() + Int32.Parse(lifeToAddStr);
        heartsBar.UpdateBarState(life - 1);
    }

    public void OnScoreChange(string scoreToAddStr) => score.text = (Int32.Parse(score.text) + Int32.Parse(scoreToAddStr)).ToString();
}

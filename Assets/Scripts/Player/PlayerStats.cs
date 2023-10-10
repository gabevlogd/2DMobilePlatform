using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : StatsBase
{
    [SerializeField]
    protected int score;

    public static int Attempts = 3;

    public virtual void OnScoreChange(string scoreToAdd) => score += Int32.Parse(scoreToAdd);
    
}

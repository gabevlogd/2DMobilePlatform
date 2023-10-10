using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsBase 
{
    [SerializeField] 
    protected int lifePoint;

    public virtual void OnLifeChange(string lifeToAdd)
    {
        lifePoint += Int32.Parse(lifeToAdd);
        if (lifePoint <= 0)
            GameManager.EventManager.TriggerEvent(Enumerators.Events.GameOver, "Lose");
    }
}

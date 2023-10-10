using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            PlayerStats.Attempts = 3;
            Player.CheckPointTaken = false;
            GameManager.EventManager.TriggerEvent(Enumerators.Events.GameOver, "Win");
        }

    }
}

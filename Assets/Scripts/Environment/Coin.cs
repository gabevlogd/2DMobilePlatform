using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            GameManager.EventManager.TriggerEvent(Enumerators.Events.ScoreChange, "10");
            Destroy(this.gameObject);
        }

    }
}

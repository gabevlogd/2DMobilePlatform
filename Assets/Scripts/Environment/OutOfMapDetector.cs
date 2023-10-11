using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapDetector : MonoBehaviour
{
    public Transform CheckPoint;
    public Transform SpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            StartCoroutine(RespawnPlayer(collision.transform));
        }
    }

    private IEnumerator RespawnPlayer(Transform player)
    {
        Camera.main.transform.parent = null;
        yield return new WaitForSeconds(2f);

        GameManager.EventManager.TriggerEvent(Enumerators.Events.LifeChange, "-1");

        if (Player.CheckPointTaken) player.transform.position = CheckPoint.position;
        else player.transform.position = SpawnPoint.position;

        Camera.main.transform.position = new Vector3(player.position.x, player.position.y, Camera.main.transform.position.z);
        Camera.main.transform.parent = player;
    }
}

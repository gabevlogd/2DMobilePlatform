using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D Rigidbody;

    protected virtual void Awake() => Rigidbody = GetComponent<Rigidbody2D>();

    protected virtual void Update() => HandleSelfDestruction();

    protected virtual void HandleSelfDestruction()
    {
        if (Vector3.Distance(transform.position, Player.GetTransform().position) > 25f) Destroy(this.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision) => DetectEnemyCollision(collision);

    protected void DetectEnemyCollision(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Destroy(collision.gameObject);
            GameManager.EventManager.TriggerEvent(Enumerators.Events.ScoreChange, "50");
            Destroy(this.gameObject);
        }
    }

    

}

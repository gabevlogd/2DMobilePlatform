using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyData Data;

    protected virtual void Awake()
    {
        Data.EnemyTransform = transform;
        Data.EnemyMovementBase = new EnemyMovementBase(ref Data);
        Data.EnemyStateMachine = new EnemyStateMachine(ref Data);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        DetectPlayerCollisions(collision);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        DetectPlayerCollisions(collision);
    }

    protected virtual void Update()
    {
        Data.EnemyStateMachine.CurrentState.OnUpdate();
    }

    protected virtual void DetectPlayerCollisions(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && Player.Hittable)
        {
            GameManager.EventManager.TriggerEvent(Enumerators.Events.LifeChange, "-1");
            GameManager.EventManager.TriggerEvent(Enumerators.Events.PlayerHitted, "2");
        }
    }
}

[System.Serializable]
public struct EnemyData
{
    public EnemyStateMachine EnemyStateMachine;
    public EnemyMovementBase EnemyMovementBase;
    [HideInInspector]
    public Transform EnemyTransform;
    //public Transform AvailableArea;
    public List<Transform> PatrolNodes;
    public bool NotAggressiveEnemy;
    public float Speed;
}

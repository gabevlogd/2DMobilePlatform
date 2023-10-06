using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public PlayerData Data;
    

    private void Awake()
    {
        Data.Transform = transform;
        Data.Rigidbody = GetComponent<Rigidbody2D>();
        Data.PlayerController = new PlayerMovement(ref Data);
        Data.PlayerStateMachine = new PlayerStateMachine(ref Data);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Data.PlayerStateMachine.CurrentState.OnCollisionEnter(collision);
    }

    private void Update()
    {
        Data.PlayerStateMachine.CurrentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        Data.PlayerStateMachine.CurrentState.OnFixedUpdate();
    }


    //public void OnMoveLeft()

}

[System.Serializable]
public struct PlayerData
{
    [HideInInspector]
    public Transform Transform;

    [HideInInspector]
    public Rigidbody2D Rigidbody;

    public PlayerStateMachine PlayerStateMachine;

    public MobileInput PlayerInput;

    public PlayerMovement PlayerController;

    

}

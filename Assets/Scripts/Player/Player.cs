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
        Data.SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        if (Input.GetKeyDown(KeyCode.D)) Data.Weapon.Shoot(Vector2.right);
        if (Input.GetKeyDown(KeyCode.A)) Data.Weapon.Shoot(Vector2.left);
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

    [HideInInspector]
    public SpriteRenderer SpriteRenderer;

    public PlayerStateMachine PlayerStateMachine;

    public MobileInput PlayerInput;

    public PlayerMovement PlayerController;

    public Weapon Weapon;

    

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public PlayerData Data;
    public static bool Hittable;
    public static bool CheckPointTaken;
    private static Player instance;
    public static Transform GetTransform() => instance.Data.Transform;

    private void Awake()
    {
        instance = this;
        Hittable = true;
        Data.Transform = transform;
        Data.Rigidbody = GetComponent<Rigidbody2D>();
        Data.Rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
        Data.SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Data.PlayerController = new PlayerMovement(ref Data);
        Data.PlayerStateMachine = new PlayerStateMachine(ref Data);

        GameManager.EventManager.Register(Enumerators.Events.LifeChange, Data.Stats.OnLifeChange);
        GameManager.EventManager.Register(Enumerators.Events.ScoreChange, Data.Stats.OnScoreChange);
        GameManager.EventManager.Register(Enumerators.Events.PlayerHitted, StartDamageCooldown);

        SpawnPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision) => Data.PlayerStateMachine.CurrentState.OnCollisionEnter(collision);

    private void Update() => Data.PlayerStateMachine.CurrentState.OnUpdate();

    private void FixedUpdate() => Data.PlayerStateMachine.CurrentState.OnFixedUpdate();

    public void StartDamageCooldown(string cooldown) => StartCoroutine(DamageCooldown(cooldown));

    private IEnumerator DamageCooldown(string cooldown)
    {
        Hittable = false;
        yield return new WaitForSeconds(Int32.Parse(cooldown));
        Hittable = true;
    }

    private void SpawnPlayer()
    {
        if (PlayerStats.Attempts > 0)
        {
            if (CheckPointTaken) transform.position = Data.CheckPoint.position;
            else transform.position = Data.SpawnPoint.position;
        }
        else if (PlayerStats.Attempts <= 0)
        {
            PlayerStats.Attempts = 3;
            CheckPointTaken = false;
            transform.position = Data.SpawnPoint.position;
        }
    }



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

    public WeaponBase Weapon;

    public PlayerStats Stats;

    public Transform SpawnPoint;
    public Transform CheckPoint;

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enumerators
{
    public enum PlayerState
    {
        MoveRight,
        MoveLeft,
        Jump,
        DoubleJump,
        Idle,
    }

    public enum EnemyState
    {
        Patrol,
        Chase
    }

    public enum Events
    {
        LifeChange,
        ScoreChange,
        PlayerHitted,
        GameOver
    }
}

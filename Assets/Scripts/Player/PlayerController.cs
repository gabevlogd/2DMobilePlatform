using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController
{
    public float Speed;
    private float startJumpVelocity;
    private float time;
    private float maxJumpHeight = 2f;

    public Rigidbody2D m_rigidbody;

    public PlayerController(ref PlayerData playerData)
    {
        m_rigidbody = playerData.Rigidbody;
        Speed = 8f;
        startJumpVelocity = Mathf.Sqrt(2f * -Physics.gravity.y * maxJumpHeight);
    }

    public void ResetTime() => time = 0f;
    public void PerformJump() => m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, startJumpVelocity - (9.81f * GetTime()));
    public void MoveLeft() => m_rigidbody.velocity = new Vector2(-Speed, m_rigidbody.velocity.y);
    public void MoveRight() => m_rigidbody.velocity = new Vector2(Speed, m_rigidbody.velocity.y);
    public void StaySill() => m_rigidbody.velocity = Vector2.zero;


    private float GetTime() => time += Time.deltaTime;
}


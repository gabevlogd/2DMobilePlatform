using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
    private float speed;
    private float decelaration;
    private float startJumpVelocity;
    private float time;
    private float maxJumpHeight = 2f;

    public Rigidbody2D m_rigidbody;

    public PlayerMovement(ref PlayerData playerData)
    {
        m_rigidbody = playerData.Rigidbody;
        speed = 8f;
        decelaration = 4f;
        startJumpVelocity = Mathf.Sqrt(2f * -Physics.gravity.y * maxJumpHeight);
    }

    public void ResetTime() => time = 0f;
    public void PerformFreeFall() => m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x * 0.8f, - (9.81f * GetTime()));
    public void PerformJump() => m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, startJumpVelocity - (9.81f * GetTime()));
    public void MoveLeft() => m_rigidbody.velocity = new Vector2(-speed, m_rigidbody.velocity.y);
    public void MoveRight() => m_rigidbody.velocity = new Vector2(speed, m_rigidbody.velocity.y);
    public void StaySill() => m_rigidbody.velocity = Vector2.zero;
    public void StaySillHorizontally()
    {
        if (m_rigidbody.velocity.x == 0) return;

        if (Mathf.Abs(m_rigidbody.velocity.x) < 0.3f)
        {
                m_rigidbody.velocity = new Vector2(0f, m_rigidbody.velocity.y);
                return;
        }
        else m_rigidbody.velocity = new Vector2(Mathf.Lerp(m_rigidbody.velocity.x, 0f, decelaration * Time.deltaTime), m_rigidbody.velocity.y);
    }

    private float GetTime() => time += Time.deltaTime;
}


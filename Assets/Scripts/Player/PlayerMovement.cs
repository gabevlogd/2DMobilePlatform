using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
    private float speed;
    private float decelaration;
    private float acceleration;
    private float startJumpVelocity;
    private float time;
    private float maxJumpHeight;

    public Rigidbody2D m_rigidbody;

    public PlayerMovement(ref PlayerData playerData)
    {
        m_rigidbody = playerData.Rigidbody;
        speed = 8f;
        decelaration = 4f;
        acceleration = 4f;
        maxJumpHeight = 2f;
        startJumpVelocity = Mathf.Sqrt(2f * -Physics.gravity.y * maxJumpHeight);
    }

    public void ResetTime() => time = 0f;
    public void PerformFreeFall() => m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, - (4.81f * GetTime()));

    /// <summary>
    /// Performs the player's fall when hitting an obstacle in the air
    /// </summary>
    public void PerformFreeFallBounce() => m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x * 0.8f, Physics2D.gravity.y * GetTime());
    public void PerformEnemyBounce() => PerformJump();
    public void PerformJump() => m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, startJumpVelocity + Physics2D.gravity.y * GetTime());
    public void StaySill() => m_rigidbody.velocity = Vector2.zero;
    public void MoveLeft() => m_rigidbody.velocity = new Vector2(Mathf.Lerp(m_rigidbody.velocity.x, -speed, acceleration * Time.deltaTime), m_rigidbody.velocity.y);
    public void MoveRight() => m_rigidbody.velocity = new Vector2(Mathf.Lerp(m_rigidbody.velocity.x, speed, acceleration * Time.deltaTime), m_rigidbody.velocity.y);

    /// <summary>
    /// Brings the speed along the horizontal axis to zero
    /// </summary>
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

    /// <summary>
    /// Performs the player's fall when stepping off a platform
    /// </summary>
    public void GravityFallDetection()
    {
        //Debug.DrawRay(m_rigidbody.transform.position + new Vector3(0.3f, 0f, 0f), Vector3.down);
        //Debug.DrawRay(m_rigidbody.transform.position + new Vector3(-0.3f, 0f, 0f), Vector3.down);

        int layerToIgnore1 = 1 << 7;
        //int layerToIgnore2 = 1 << 8;
        RaycastHit2D hitInfoRight = Physics2D.Raycast(m_rigidbody.transform.position + new Vector3(0.3f, 0f, 0f), Vector2.down, Mathf.Infinity, ~layerToIgnore1/* | ~layerToIgnore2*/);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(m_rigidbody.transform.position + new Vector3(-0.3f, 0f, 0f), Vector2.down, Mathf.Infinity, ~layerToIgnore1/* | ~layerToIgnore2*/);
        if (hitInfoRight.distance >= 0.6f && hitInfoLeft.distance >= 0.6f) PerformFreeFall();
    }

    private float GetTime() => time += Time.deltaTime;
}


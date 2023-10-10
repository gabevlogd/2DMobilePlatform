using Gabevlogd.Patterns;
using UnityEngine;

public class Jump : PlayerState
{
    protected MobileInput playerInput;
    protected PlayerMovement playerController;
    private SpriteRenderer spriteRenderer;

    protected bool inAir;
    protected bool ceilingHitted;
    protected bool isBouncingLeft;
    protected bool isBouncingRight;
    protected float startBounceTime;
    protected float inputDelayTime;

    public Jump(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        inputDelayTime = 1f;
        playerInput = m_playerStateMachine.PlayerData.PlayerInput;
        playerController = m_playerStateMachine.PlayerData.PlayerController;
        spriteRenderer = m_playerStateMachine.PlayerData.SpriteRenderer;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        playerInput.JumpButton.IsPressed = false; //need attention
        inAir = true;

        HandleInput();
        playerController.ResetTime();
    }

    public override void OnExit()
    {
        base.OnExit();
        inAir = false;
        ceilingHitted = false;
    }

    public override void OnCollisionEnter(Collision2D collision)
    {
        base.OnCollisionEnter(collision);
        HandleCollisions(collision);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        if (ceilingHitted) playerController.PerformFreeFallBounce();
        else playerController.PerformJump();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleInput();
        CheckHorizontalInput();
    }

    protected override void HandleInput()
    {
        base.HandleInput();

        if (!inAir && !playerInput.AnyInput())
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.Idle);
            return;
        }

        if (playerInput.JumpButton.IsPressed && inAir)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.DoubleJump);
            return;
        }

        if (playerInput.LeftButton.IsPressed && !inAir)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.MoveLeft);
            return;
        }

        if (playerInput.RightButton.IsPressed && !inAir)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.MoveRight);
            return;
        }
    }

    /// <summary>
    /// Manages the horizontal movement during the jump
    /// </summary>
    protected void CheckHorizontalInput()
    {
        if (!playerInput.LeftButton.IsPressed && !playerInput.RightButton.IsPressed) playerController.StaySillHorizontally();

        if (isBouncingLeft)
        {
            playerController.MoveLeft();
            if ((Time.time - startBounceTime) >= inputDelayTime) isBouncingLeft = false;
            return;
        }
        else if (isBouncingRight)
        {
            playerController.MoveRight();
            if ((Time.time - startBounceTime) >= inputDelayTime) isBouncingRight = false;
            return;
        }

        if (playerInput.LeftButton.IsPressed)
        {
            playerController.MoveLeft();
            if (!spriteRenderer.flipX) spriteRenderer.flipX = true;
        }

        if (playerInput.RightButton.IsPressed)
        {
            playerController.MoveRight();
            if (spriteRenderer.flipX) spriteRenderer.flipX = false;
        }
    }

    /// <summary>
    /// Handles the horizontal and vertical collision events
    /// </summary>
    /// <param name="collision"></param>
    protected void HandleCollisions(Collision2D collision)
    {
        if (collision.GetContact(0).normal.x > 0.7f)
        {
            //Debug.Log("1");
            startBounceTime = Time.time;
            isBouncingRight = true;
        }
        else if (collision.GetContact(0).normal.x < -0.7f)
        {
            //Debug.Log("2");
            startBounceTime = Time.time;
            isBouncingLeft = true;
        }

        if (collision.GetContact(0).normal.y > 0.5f)
        {
            if (collision.gameObject.layer == 6)
            {
                //Debug.Log("3");
                inAir = false;
                ceilingHitted = false;
                return;
            }

        }
        else if (collision.GetContact(0).normal.y < 0.5f)
        {
            //Debug.Log("4");
            ceilingHitted = true;
            playerController.ResetTime();
        }
    }

}

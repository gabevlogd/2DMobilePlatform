using Gabevlogd.Patterns;
using UnityEngine;

public class Jump : PlayerState
{
    protected MobileInput playerInput;
    protected PlayerMovement playerController;

    protected bool inAir;
    protected bool ceilingHitted;
    protected bool isBouncing;
    protected float startBounceTime;
    protected float inputDelayTime;

    public Jump(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        inputDelayTime = 1f;
        playerInput = m_playerStateMachine.PlayerData.PlayerInput;
        playerController = m_playerStateMachine.PlayerData.PlayerController;
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

        if (collision.GetContact(0).normal.x > 0.7f)
        {
            playerController.MoveRight();
            startBounceTime = Time.time;
            isBouncing = true;
        }

        if (collision.GetContact(0).normal.x < -0.7f)
        {
            playerController.MoveLeft();
            startBounceTime = Time.time;
            isBouncing = true;
        }

        if (collision.GetContact(0).normal.y > 0.5f)
        {
            if (collision.gameObject.layer == 6)
            {
                inAir = false;
                ceilingHitted = false;
                return;
            }

        }
        else if (collision.GetContact(0).normal.y < 0.5f)
        {
            ceilingHitted = true;
            playerController.ResetTime();
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        if (ceilingHitted) playerController.PerformFreeFall();
        else playerController.PerformJump();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleInput();
        CheckHorizzontalInput();
    }

    public override void HandleInput()
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

    protected void CheckHorizzontalInput()
    {
        if (!playerInput.LeftButton.IsPressed && !playerInput.RightButton.IsPressed) playerController.StaySillHorizontally();

        if (isBouncing)
        {
            if ((Time.time - startBounceTime) >= inputDelayTime) isBouncing = false;
            return;
        }

        if (playerInput.LeftButton.IsPressed) playerController.MoveLeft();
        if (playerInput.RightButton.IsPressed) playerController.MoveRight();
    }

}

using Gabevlogd.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe this state is not really needed cause for the moment is identical to the jump state, but I'll wait to refactoring cause I'm not sure
public class DoubleJump : PlayerState
{
    private MobileInput playerInput;
    private PlayerController playerController;
    private bool inAir;

    public DoubleJump(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
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

    public override void OnCollisionEnter(Collision2D collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.layer == 6) inAir = false;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        playerController.PerformJump();
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

    private void CheckHorizzontalInput()
    {
        if (playerInput.LeftButton.IsPressed) playerController.MoveLeft();
        if (playerInput.RightButton.IsPressed) playerController.MoveRight();
    }

}

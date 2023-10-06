using Gabevlogd.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{
    private MobileInput playerInput;
    private PlayerMovement playerController;

    public Idle(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        playerInput = m_playerStateMachine.PlayerData.PlayerInput;
        playerController = m_playerStateMachine.PlayerData.PlayerController;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        HandleInput();
        playerController.StaySill();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleInput();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (playerInput.LeftButton.IsPressed)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.MoveLeft);
            return;
        }
        
        if (playerInput.RightButton.IsPressed)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.MoveRight);
            return;
        }

        if (playerInput.JumpButton.IsPressed)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.Jump);
            return;
        }
    }

}

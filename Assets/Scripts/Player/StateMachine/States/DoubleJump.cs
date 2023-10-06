using Gabevlogd.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe this state is not really needed cause for the moment is identical to the jump state, but I'll wait to refactoring cause I'm not sure
public class DoubleJump : Jump
{

    public DoubleJump(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        inputDelayTime = 1f;
        playerInput = m_playerStateMachine.PlayerData.PlayerInput;
        playerController = m_playerStateMachine.PlayerData.PlayerController;
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

}

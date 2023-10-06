using Gabevlogd.Patterns;
using UnityEngine;

public class Left : PlayerState
{
    private MobileInput playerInput;
    private PlayerMovement playerController;

    public Left(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        playerInput = m_playerStateMachine.PlayerData.PlayerInput;
        playerController = m_playerStateMachine.PlayerData.PlayerController;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        HandleInput();
        playerController.MoveLeft();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleInput();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (!playerInput.AnyInput())
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.Idle);
            return;
        }

        if (playerInput.JumpButton.IsPressed)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.Jump);
            return;
        }

        if (playerInput.RightButton.IsPressed && !playerInput.LeftButton.IsPressed)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.MoveRight);
            return;
        }
    }

}

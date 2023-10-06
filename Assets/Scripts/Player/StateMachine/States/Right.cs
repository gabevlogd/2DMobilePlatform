using Gabevlogd.Patterns;
using UnityEngine;

public class Right : PlayerState
{
    private MobileInput playerInput;
    private PlayerController playerController;

    public Right(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        playerInput = m_playerStateMachine.PlayerData.PlayerInput;
        playerController = m_playerStateMachine.PlayerData.PlayerController;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        HandleInput();
        playerController.MoveRight();
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

        if (playerInput.LeftButton.IsPressed && !playerInput.RightButton.IsPressed)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.MoveLeft);
            return;
        }
    }

}

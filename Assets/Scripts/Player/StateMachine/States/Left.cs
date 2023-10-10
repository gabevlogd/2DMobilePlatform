using Gabevlogd.Patterns;
using UnityEngine;

public class Left : PlayerState
{
    private MobileInput playerInput;
    private PlayerMovement playerController;
    private SpriteRenderer spriteRenderer;
    private WeaponBase playerWeapon;

    public Left(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        playerInput = m_playerStateMachine.PlayerData.PlayerInput;
        playerController = m_playerStateMachine.PlayerData.PlayerController;
        spriteRenderer = m_playerStateMachine.PlayerData.SpriteRenderer;
        playerWeapon = m_playerStateMachine.PlayerData.Weapon;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        HandleInput();
        spriteRenderer.flipX = true;

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleInput();
        playerController.MoveLeft();
        playerController.GravityFallDetection();
        if (playerInput.AttackButton.IsPressed) playerWeapon.Shoot(Vector2.left);
    }

    protected override void HandleInput()
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

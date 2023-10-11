using Gabevlogd.Patterns;
using UnityEngine;

public class Right : PlayerState
{
    private MobileInput playerInput;
    private PlayerMovement playerController;
    private SpriteRenderer spriteRenderer;
    private WeaponBase playerWeapon;

    public Right(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
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
        spriteRenderer.flipX = false;        
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleInput();
        playerController.MoveRight();
        if (playerInput.AttackButton.IsPressed) playerWeapon.Shoot(Vector2.right);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        playerController.GravityFallDetection();
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

        if (playerInput.LeftButton.IsPressed && !playerInput.RightButton.IsPressed)
        {
            m_playerStateMachine.ChangeState(Enumerators.PlayerState.MoveLeft);
            return;
        }
    }

}

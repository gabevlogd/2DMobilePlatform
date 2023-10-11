using Gabevlogd.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{
    private MobileInput playerInput;
    private PlayerMovement playerController;
    private WeaponBase playerWeapon;
    private SpriteRenderer spriteRenderer;

    public Idle(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        playerInput = m_playerStateMachine.PlayerData.PlayerInput;
        playerController = m_playerStateMachine.PlayerData.PlayerController;
        playerWeapon = m_playerStateMachine.PlayerData.Weapon;
        spriteRenderer = m_playerStateMachine.PlayerData.SpriteRenderer;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        HandleInput();
        
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleInput();
        playerController.StaySillHorizontally();
        CheckShootTrigger();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        playerController.GravityFallDetection();
    }

    protected override void HandleInput()
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

    private void CheckShootTrigger()
    {
        if (playerInput.AttackButton.IsPressed)
        {
            if (spriteRenderer.flipX) playerWeapon.Shoot(Vector2.left);
            else playerWeapon.Shoot(Vector2.right);
        }
    }

}

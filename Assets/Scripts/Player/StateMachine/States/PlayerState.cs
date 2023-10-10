using Gabevlogd.Patterns;
using UnityEngine;

public class PlayerState : State<Enumerators.PlayerState>
{
    protected PlayerStateMachine m_playerStateMachine;
    //protected bool enemyBounce;

    public PlayerState(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        m_playerStateMachine = m_stateManager as PlayerStateMachine;
    }

    //public override void OnCollisionEnter(Collision2D collision)
    //{
    //    base.OnCollisionEnter(collision);
    //    if (collision.gameObject.layer == 8)
    //    {
    //        if (collision.GetContact(0).normal.y >= 0.1f)
    //        {
    //            enemyBounce = true;
    //            m_playerStateMachine.PlayerData.PlayerController.ResetTime();
    //        }
    //    }
    //    else if (enemyBounce) enemyBounce = false;
    //}

    //public override void OnFixedUpdate()
    //{
    //    base.OnFixedUpdate();
    //    if (enemyBounce) m_playerStateMachine.PlayerData.PlayerController.PerformEnemyBounce();
    //}

    /// <summary>
    /// Checks the input looking for change state requests
    /// </summary>
    protected virtual void HandleInput()
    {
        //Debug.Log("HandleInput");
    }

    
}

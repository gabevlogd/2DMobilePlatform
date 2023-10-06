using Gabevlogd.Patterns;
using UnityEngine;

public class PlayerState : State<Enumerators.PlayerState>
{
    protected PlayerStateMachine m_playerStateMachine;
    protected Transform m_playerTransform;

    public PlayerState(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        m_playerStateMachine = m_stateManager as PlayerStateMachine;
        m_playerTransform = m_playerStateMachine.PlayerData.Transform;
    }

    public virtual void HandleInput()
    {
        //Debug.Log("HandleInput");
    }

    
}

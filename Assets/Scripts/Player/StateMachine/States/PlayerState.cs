using Gabevlogd.Patterns;
using UnityEngine;

public class PlayerState : State<Enumerators.PlayerState>
{
    protected PlayerStateMachine m_playerStateMachine;

    public PlayerState(Enumerators.PlayerState stateID, StatesManager<Enumerators.PlayerState> stateManager) : base(stateID, stateManager)
    {
        m_playerStateMachine = m_stateManager as PlayerStateMachine;
    }

    public virtual void HandleInput()
    {
        //Debug.Log("HandleInput");
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class EnemyState : State<Enumerators.EnemyState>
{
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState(Enumerators.EnemyState stateID, StatesManager<Enumerators.EnemyState> stateManager) : base(stateID, stateManager)
    {
        enemyStateMachine = m_stateManager as EnemyStateMachine;
    }

    /// <summary>
    /// Looks for change state condition
    /// </summary>
    protected virtual void HandleChangeState()
    {
        //Debug.Log("HandleChangeState");
    }
}

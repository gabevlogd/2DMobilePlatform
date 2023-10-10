using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class EnemyStateMachine : StatesManager<Enumerators.EnemyState>
{
    public EnemyData EnemyData;

    public EnemyStateMachine(ref EnemyData enemyData)
    {
        EnemyData = enemyData;
        InitStatesManager();
    }

    protected override void InitStates()
    {
        AllStates.Add(Enumerators.EnemyState.Patrol, new Patrol(Enumerators.EnemyState.Patrol, this));
        AllStates.Add(Enumerators.EnemyState.Chase, new Chase(Enumerators.EnemyState.Chase, this));

        CurrentState = AllStates[Enumerators.EnemyState.Patrol];
        CurrentState.OnEnter();
    }
}

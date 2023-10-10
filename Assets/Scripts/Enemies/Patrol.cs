using Gabevlogd.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : EnemyState
{
    private EnemyMovementBase enemyController;
    private Transform enemyTransform;
    private float triggerChaseDistance;

    public Patrol(Enumerators.EnemyState stateID, StatesManager<Enumerators.EnemyState> stateManager) : base(stateID, stateManager)
    {
        triggerChaseDistance = 7f;
        enemyController = enemyStateMachine.EnemyData.EnemyMovementBase;
        enemyTransform = enemyStateMachine.EnemyData.EnemyTransform;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        HandleChangeState();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleChangeState();
        enemyController.PerformPatrol();
    }

    protected override void HandleChangeState()
    {
        base.HandleChangeState();
        if (enemyStateMachine.EnemyData.NotAggressiveEnemy) return;
        if (Vector3.Distance(Player.GetTransform().position, enemyTransform.position) <= triggerChaseDistance)
            enemyStateMachine.ChangeState(Enumerators.EnemyState.Chase);
    }
}

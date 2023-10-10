using Gabevlogd.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : EnemyState
{
    //private Vector3 walkableAreaPivot;
    private EnemyMovementBase enemyController;
    private Transform enemyTransform;
    //private float maxDistanceFromWalkableArea;
    private float maxDistanceFromPlayer;

    public Chase(Enumerators.EnemyState stateID, StatesManager<Enumerators.EnemyState> stateManager) : base(stateID, stateManager)
    {
        //walkableAreaPivot = enemyStateMachine.EnemyData.AvailableArea.position;
        enemyController = enemyStateMachine.EnemyData.EnemyMovementBase;
        enemyTransform = enemyStateMachine.EnemyData.EnemyTransform;
        //maxDistanceFromWalkableArea = 12f;
        maxDistanceFromPlayer = 11f;
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
        enemyController.PerformChase();
    }

    protected override void HandleChangeState()
    {
        base.HandleChangeState();
        //if (Vector3.Distance(enemyTransform.position, walkableAreaPivot) >= maxDistanceFromWalkableArea)
        //    enemyStateMachine.ChangeState(Enumerators.EnemyState.Patrol);
        /*else */if (Vector3.Distance(enemyTransform.position, Player.GetTransform().position) >= maxDistanceFromPlayer)
            enemyStateMachine.ChangeState(Enumerators.EnemyState.Patrol);

    }
}

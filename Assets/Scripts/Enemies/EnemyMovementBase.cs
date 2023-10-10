using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBase
{
    private Transform enemyTransform;
    private List<Vector3> patrolNodes;
    private Vector3 targetPatrolNode;
    private int patrolNodesIndex;
    private float speed;

    public EnemyMovementBase(ref EnemyData enemyData)
    {
        enemyTransform = enemyData.EnemyTransform;
        patrolNodesIndex = 0;
        speed = enemyData.Speed;
        patrolNodes = new List<Vector3>();
        foreach (Transform node in enemyData.PatrolNodes)
        {
            patrolNodes.Add(node.position);
        }
        targetPatrolNode = patrolNodes[patrolNodesIndex];
    }

    public void PerformChase()
    {
        Vector3 target = new Vector3(Player.GetTransform().position.x, enemyTransform.position.y, enemyTransform.position.z);
        if (Vector3.Distance(target, enemyTransform.position) > 0.4f)
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, target, Time.deltaTime * speed);
    }

    public void PerformPatrol()
    {
        if (Vector3.Distance(enemyTransform.position, targetPatrolNode) < 0.1f) 
            targetPatrolNode = GetNewNode();

        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, targetPatrolNode, Time.deltaTime * speed);
    }

    /// <summary>
    /// Returns a new patrol node different from the current
    /// </summary>
    private Vector3 GetNewNode()
    {
        if (patrolNodesIndex == patrolNodes.Count - 1) patrolNodesIndex = 0;
        else patrolNodesIndex++;

        return patrolNodes[patrolNodesIndex];
    }
}

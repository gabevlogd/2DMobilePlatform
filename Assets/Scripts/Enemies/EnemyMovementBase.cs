using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBase
{
    private Transform enemyTransform;
    private Rigidbody2D rigidbody;
    private List<Vector3> patrolNodes;
    private Vector3 targetPatrolNode;
    private int patrolNodesIndex;
    private float speed;
    private float time;

    public EnemyMovementBase(ref EnemyData enemyData)
    {
        rigidbody = enemyData.Rigidbody;
        enemyTransform = enemyData.EnemyTransform;
        patrolNodesIndex = 0;
        speed = enemyData.Speed;
        patrolNodes = new List<Vector3>();
        if (enemyData.PatrolNodes.Count >= 1)
        {
            foreach (Transform node in enemyData.PatrolNodes)
            {
                patrolNodes.Add(node.position);
            }
            targetPatrolNode = patrolNodes[patrolNodesIndex];
        }
        
    }

    public void PerformChase()
    {
        Vector3 target = new Vector3(Player.GetTransform().position.x, enemyTransform.position.y, enemyTransform.position.z);
        if (Vector3.Distance(target, enemyTransform.position) > 0.4f)
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, target, Time.deltaTime * speed);
    }

    public void PerformPatrol()
    {
        if (patrolNodes.Count <= 1) return;
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

    public void GravityFallDetection()
    {
        int layerToIgnore1 = 1 << 8;
        RaycastHit2D hitInfoRight = Physics2D.Raycast(rigidbody.transform.position + new Vector3(0.3f, 0f, 0f), Vector2.down, Mathf.Infinity, ~layerToIgnore1);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(rigidbody.transform.position + new Vector3(-0.3f, 0f, 0f), Vector2.down, Mathf.Infinity, ~layerToIgnore1);
        if (hitInfoRight.distance >= 0.6f && hitInfoLeft.distance >= 0.6f) PerformFreeFall();
    }

    public void PerformFreeFall() => rigidbody.velocity = new Vector2(rigidbody.velocity.x, -9.81f * GetTime());
    private float GetTime() => time += Time.deltaTime;
}

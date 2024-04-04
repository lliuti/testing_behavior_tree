using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BehaviorTree;

public class GoblinBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    public UnityEngine.LayerMask enemiesLayer;
    public static float speed = 3f;
    public static int attackDamage = 10;
    public static float attackRange = 2f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node> 
        {
            new Sequence(new List<Node> 
            {
                new CheckInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node> 
            {
                new CheckNearbyEnemy(transform, enemiesLayer),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints)
        });
        return root;
    }
}

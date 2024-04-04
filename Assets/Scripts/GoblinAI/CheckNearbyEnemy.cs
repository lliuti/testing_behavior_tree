using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckNearbyEnemy : Node
{
    private Transform _transform;
    private LayerMask _enemiesLayer;

    public CheckNearbyEnemy(Transform transform, LayerMask enemiesLayer) 
    {
        _transform = transform;
        _enemiesLayer = enemiesLayer;
    }

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        if (targetObject == null)
        {
            RaycastHit2D[] hitEntities = Physics2D.RaycastAll(_transform.position, _transform.right, 4f, _enemiesLayer);
            if (hitEntities.Length > 0)
            {
                parent.parent.SetData("target", hitEntities[0].transform);
                Debug.Log($"Found: {hitEntities[0].transform.name}");

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}

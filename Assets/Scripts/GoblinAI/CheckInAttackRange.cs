using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckInAttackRange : Node
{
    private Transform _transform;
    private Animator _animator;

    public CheckInAttackRange(Transform transform) 
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        if (targetObject == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)targetObject;

        float distance = target.position.x - _transform.position.x;
        if (Mathf.Abs(distance) <= GoblinBT.attackRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    private Animator _animator;

    private bool _isFacingRight = true;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        float distance = target.position.x - _transform.position.x;

        if (Mathf.Abs(distance) > 0.01f)
        {
            _transform.position = Vector2.MoveTowards(_transform.position, target.position, GoblinBT.speed * Time.deltaTime);
            _animator.Play("Run");

            if ((distance > 0 && !_isFacingRight) || (distance < 0 && _isFacingRight))
            {
                Quaternion rotation = _transform.rotation;
                Vector3 rotator = new Vector3(rotation.x, _isFacingRight ? 180f : 0f, rotation.z);
                _transform.rotation = Quaternion.Euler(rotator);
                _isFacingRight = !_isFacingRight;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}

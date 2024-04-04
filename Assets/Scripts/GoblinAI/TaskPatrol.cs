using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;
    private Animator _animator;
    private int _currentWaypointIndex = 0;

    private bool _isFacingRight = true;

    private float _waitTime = 2f;
    private float _waitCounter;
    private bool _waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        Transform currentWaypoint = _waypoints[_currentWaypointIndex];

        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
            }
        }
        else 
        {
            float distance = currentWaypoint.position.x - _transform.position.x;

            if (Mathf.Abs(distance) > 0.5f)
            {
                _transform.position = Vector2.MoveTowards(_transform.position, currentWaypoint.position, GoblinBT.speed * Time.deltaTime);
                _animator.Play("Run");

                if ((distance > 0 && !_isFacingRight) || (distance < 0 && _isFacingRight))
                {
                    Quaternion rotation = _transform.rotation;
                    Vector3 rotator = new Vector3(rotation.x, _isFacingRight ? 180f : 0f, rotation.z);
                    _transform.rotation = Quaternion.Euler(rotator);
                    _isFacingRight = !_isFacingRight;
                }
            }
            else 
            {
                _waitCounter = 0f;
                _waiting = true;

                _currentWaypointIndex = _currentWaypointIndex + 1 < _waypoints.Length ? _currentWaypointIndex + 1 : 0;
                _animator.Play("Idle");
            }
        }


        state = NodeState.RUNNING;
        return state;
    }
}

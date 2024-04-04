using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttack : Node
{
    private Transform _lastTarget;
    private Transform _transform;
    private Animator _animator;
    private EnemyManager _enemyManager;
    private bool _isTargetDead;

    private float _attackCounter = 0f;
    private float _attackTime = 2f;

    private bool _firstAttack = true;

    public TaskAttack(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();    
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_firstAttack || _attackCounter >= _attackTime)
        {
            _isTargetDead = _enemyManager.TakeHit(GoblinBT.attackDamage);
            if (_isTargetDead)
            {
                ClearData("target");
                _animator.Play("Run");
            }   
            else 
            {
                _attackCounter = 0f;
            }
            _firstAttack = false;
            _animator.Play("Attack");
        }

        state = NodeState.RUNNING;
        return state;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    
    public override StateType Type { get; }
    
    private float _attackDelay = 0;
    private EnemyAttackType _attackType;
    public AttackState() : base("Attack") { }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
        fms.TriggerAnimation("Attack");
        _attackDelay = fms.Config.AttackDelay;
        _attackType = fms.Config.AttackType;
        SetStateDuration(fms.Config.AttackDuration);
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
        if (_attackDelay > 0)
        {
            _attackDelay -= deltaTime;
            if (_attackDelay <= 0)
            {
                //Apply Damage
                BasicAttack(fms,deltaTime);
            }
        }
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }

    private void BasicAttack(FiniteStateMachine fms, float deltaTime)
    {
        if (fms.Target.TryGetComponent(out IDamageable target))
        {
            target.TakeHit();
        }

        // No siempre queremos que suene el zombie
        if (GameManager.Instance.gameState == GameState.InGame)
          {
               if (Random.Range(0f, 1f) <= 0.7)
                    AudioManager.Instance.PlaySound2D("Zombie");
          }
               
    }

    private void ExplodeAttack(FiniteStateMachine fms, float deltaTime)
    {
        Collider[] collidersInRange = Physics.OverlapSphere(fms.transform.position + Vector3.up * 0.5f,fms.Config.AttackRange);
            
            foreach (var c in collidersInRange)
            {
                if (c.TryGetComponent(out IDamageable target))
                {
                    target.TakeHit();
                }
            }
    }
}

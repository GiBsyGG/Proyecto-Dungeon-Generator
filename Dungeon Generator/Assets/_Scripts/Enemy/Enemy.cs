using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
     private FiniteStateMachine _fms;
     private EnemyConfig _config;

     void Start()
     {
          InitHealth();

          _config = GetComponent<EnemyConfig>();
          _fms = GetComponent<FiniteStateMachine>();
     }

     protected override void OnDeath()
     {
          base.OnDeath();
          _fms.ToState(StateType.Dead);

          //TODO: Communicate Enemy death
          //TODO: Delete Death enemies in the enemies room list
          GameEvents.OnEnemyDeath?.Invoke(this);
     }
}

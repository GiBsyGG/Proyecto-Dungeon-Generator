using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class ChaseState : State
{
    public override StateType Type => StateType.Chase;

   //private float _navMeshRefreshTimer = 0;

    public ChaseState() : base("Chase") { }
    
    protected  override void OnEnterState(FiniteStateMachine fms)
    {
        //_navMeshRefreshTimer = 0;
        fms.SetMovementSpeed(fms.Config.ChaseSpeed);
        fms.MovementController.SetTarget(fms.Target);
    }

    protected  override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
       fms.MovementController.GoToTarget();
    }

    protected  override void OnExitState(FiniteStateMachine fms)
    {
       //fms.NavMeshController.StopAgent();
    }
}

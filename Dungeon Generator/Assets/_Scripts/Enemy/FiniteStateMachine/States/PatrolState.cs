using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PatrolState : State
{
    public override StateType Type => StateType.Patrol;
    
    

    public PatrolState() : base("Patrol") { }

    //private Rigidbody2D _rb;
    
    protected override void OnEnterState(FiniteStateMachine fms)
    {
        
        fms.SetMovementSpeed(fms.Config.Speed);
        //_rb = GetComponent<Rigidbody2D>();
        
        
    }
    
    

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
        fms.MovementController.GoToNextWaypoint();

    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
        fms.MovementController.StopAgent();
    }
}

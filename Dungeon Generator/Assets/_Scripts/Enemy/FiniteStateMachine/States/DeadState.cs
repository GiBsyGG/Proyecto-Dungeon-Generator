using UnityEngine;

public class DeadState : State
{
    public override StateType Type => StateType.Dead;

    public DeadState() : base("Dead") { }
    public float delay;
    protected override void OnEnterState(FiniteStateMachine fms)
    {
        fms.TriggerAnimation("Dead");
        delay = fms.Config.DeadDelay;
        fms.MovementController.StopAgent();
        fms.TryGetComponent<BoxCollider2D> (out var boxCollider);
        boxCollider.enabled = false;
        
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
        
        if (delay > 0)
        {
            
            delay -= deltaTime;
            if (delay <= 0)
            {
               MonoBehaviour.Destroy(fms.gameObject);
            }
        }
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
        
    }
}
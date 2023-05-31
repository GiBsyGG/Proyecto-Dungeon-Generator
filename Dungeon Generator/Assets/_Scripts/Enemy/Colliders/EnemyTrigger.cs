using UnityEngine;
using UnityEngine.Events;

public class EnemyTrigger : MonoBehaviour
{

    [SerializeField] 
    //private bool _disableOnDetection;
    FiniteStateMachine fms;
    //public UnityEvent OnDetection;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("lugador cerca");
        //_rb.velocity = _rb.velocity*-1;
        //fms.MovementController.TurnAround();
        if (other.name=="WorldCollider")
        {
            print("player");
            //fms.SetMovementSpeed(fms.Config.Speed*-1);
        }
    }
}
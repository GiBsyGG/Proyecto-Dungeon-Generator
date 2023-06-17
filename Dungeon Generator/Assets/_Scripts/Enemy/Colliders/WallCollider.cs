using UnityEngine;
using UnityEngine.Events;

public class WallCollider : MonoBehaviour
{

    [SerializeField] 
    //private bool _disableOnDetection;
    FiniteStateMachine fms;
    //public UnityEvent OnDetection;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        //_rb.velocity = _rb.velocity*-1;
        if(other.collider.name == "Walls" && fms.getCurrentState() == StateType.Patrol)
        {
            fms.MovementController.TurnAround();
        }
        
        /*if (other.CompareTag("Walls"))
        {
            print("pared");
            //fms.SetMovementSpeed(fms.Config.Speed*-1);
        }*/
    }
}
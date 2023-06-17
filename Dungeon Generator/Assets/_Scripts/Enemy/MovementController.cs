using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    //[SerializeField] private Transform[] _waypointObjects;
    //[SerializeField] [HideInInspector] private Vector3[] _waypoints;
    private int _changeDir;
    [SerializeField]
    private GameObject _player;
    private Transform _target;  
    private System.Random random;
    private int _currentWaypoint;
    private Transform _waypoint;
    public List<Vector2> direcciones;
    //private NavMeshAgent _agent;
    private Rigidbody2D _rb;
    private float _speed;

    //public bool IsMoving => _agent != null && _agent.velocity.magnitude > 0.1f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start(){
        random = new System.Random();
        _currentWaypoint = random.Next(9);
        _changeDir = 0;
        direcciones.Add(new Vector2(0, 0));//detener animacion
        direcciones.Add(new Vector2(0, 1));
        direcciones.Add(new Vector2(0, -1));
        direcciones.Add(new Vector2(1, 0));
        direcciones.Add(new Vector2(-1, 0));//4
        direcciones.Add(new Vector2(1, 1));
        direcciones.Add(new Vector2(-1,-1));//6
        direcciones.Add(new Vector2(1,-1));
        direcciones.Add(new Vector2(-1,-1));//8
        
    }
    

    public void SetTarget(Transform target) => _target = target;

    public void SetSpeed(float speed) => _speed = speed;
    
    public void GoToNextWaypoint(){ 
        _changeDir++; 
        
        Vector2 _dir = direcciones[_currentWaypoint];
        _dir.Normalize();
        _rb.velocity = _dir * _speed;
        if(_changeDir > 150)
        {
            _currentWaypoint = random.Next(9);
            
            _changeDir = 0;
        }
        AnimRotation();
       // _agent.SetDestination(_waypoints[_currentWaypoint]);
        //_agent.isStopped = false;
        //_currentWaypoint++;  
        //if (_currentWaypoint >= _waypoints.Length)  
        //    _currentWaypoint = 0;  
    }
    
    public void TurnAround(){ 
        _speed = _speed*-1;
        
    }

    public void GoToTarget(){  
        Vector2 _dirChase = (Vector2)transform.position - (Vector2)_target.position;
        
        _dirChase.Normalize();
        _rb.velocity = _dirChase * Math.Abs(_speed)*-1;
        AnimRotation();
        //transform.position = Vector2.MoveTowards(transform.position,_target.position,_speed *Time.deltaTime);
        //_agent.isStopped = false;
    }
    
    public void StopAgent(){  
        //_agent.isStopped = true;
        _rb.velocity = Vector2.zero;
        //_agent.ResetPath();  
    }
    
   public void AnimRotation()
   {
    if(_rb.velocity.x < 0)
    {
        
        transform.rotation = Quaternion.Euler(0, 180, 0);
    } else {
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
   }
}
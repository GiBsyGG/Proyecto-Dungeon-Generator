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
        _currentWaypoint = random.Next(8);
        _changeDir = 0;
        direcciones.Add(new Vector2(0, 1));
        direcciones.Add(new Vector2(0, -1));
        direcciones.Add(new Vector2(1, 0));
        direcciones.Add(new Vector2(-1, 0));
        direcciones.Add(new Vector2(1, 1));
        direcciones.Add(new Vector2(-1,-1));
        direcciones.Add(new Vector2(1,-1));
        direcciones.Add(new Vector2(-1,-1));  

        /*_waypoints = new Vector3[_waypointObjects.Length];
        for (int i = 0; i < _waypointObjects.Length; i++)
        {
            _waypoints[i] = _waypointObjects[i].position;
            _waypointObjects[i].gameObject.SetActive(false);
        }*/
        
    }
    

    public void SetTarget(Transform target) => _target = target;

    public void SetSpeed(float speed) => _speed = speed;
    
    public void GoToNextWaypoint(){ 
        _changeDir++; 
        //print(_changeDir);
        Vector2 _dir = direcciones[_currentWaypoint];
        _dir.Normalize();
        _rb.velocity = _dir * _speed;
        if(_changeDir > 150)
        {
            _currentWaypoint = random.Next(8);
            //print(_currentWaypoint);
            _changeDir = 0;
        }
       // _agent.SetDestination(_waypoints[_currentWaypoint]);
        //_agent.isStopped = false;
        //_currentWaypoint++;  
        //if (_currentWaypoint >= _waypoints.Length)  
        //    _currentWaypoint = 0;  
    }
    
    public void TurnAround(){ 
        _speed = _speed*-1;
        print("turn");
        print(_speed);
    }

    public void GoToTarget(){  
        //_agent.SetDestination(_target.position);
        transform.position = Vector2.MoveTowards(transform.position,_target.position,_speed *Time.deltaTime);
        //_agent.isStopped = false;
    }
    
    public void StopAgent(){  
        //_agent.isStopped = true;
        _rb.velocity = Vector2.zero;
        //_agent.ResetPath();  
    }
    
    /*public bool IsAtDestination()
    {
        if (_agent.pathPending)
            return false;

        if (_agent.remainingDistance > _agent.stoppingDistance)
            return false;

        return !_agent.hasPath || _agent.velocity.sqrMagnitude == 0f;
    }*/

    /*private void OnValidate()
    {
        _waypoints = new Vector3[_waypointObjects.Length];
        for (int i = 0; i < _waypointObjects.Length; i++)
        {
            _waypoints[i] = _waypointObjects[i].position;
        }
    }*/
}
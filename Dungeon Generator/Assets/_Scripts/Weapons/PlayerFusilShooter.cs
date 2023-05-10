using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class PlayerFusilShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _shootPoint;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Shoot
            GameObject projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = _shootPoint.position;
            projectile.transform.rotation = _shootPoint.rotation;
        }
    }
}
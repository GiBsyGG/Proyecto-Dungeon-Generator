using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{ 
    [SerializeField]
    private Transform _muzzle;
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private float _msBetweenShots =  500;
    [SerializeField]
    private float _muzzleVelocity = 10;
    
    float nextShotTime;


    public void Shoot(){

        if (Time.time > nextShotTime){
        nextShotTime = Time.time + _msBetweenShots / 1000;
        GameObject newProjectile = Instantiate(_projectile, _muzzle.position, _muzzle.rotation);  
          newProjectile.TryGetComponent<Bullet>(out Bullet bullet);
          if (bullet != null){
               bullet.SetSpeed(_muzzleVelocity);
          }  
        }
    }

}

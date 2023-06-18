using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{ 
    public enum FireMode{Auto, Burst, Single};
    public FireMode fireMode;
    [SerializeField]
    private Transform[]  _projectileSpawn;
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private float _msBetweenShots =  500;
    [SerializeField]
    private float _muzzleVelocity = 10;
    [SerializeField]
    private int  _burstCount;


    float nextShotTime;

    bool triggerRelaesedSinceLastShot;

    int shotsRemainingInBurst;

    void Start(){
      shotsRemainingInBurst =  _burstCount;
    }

    void Shoot(){

        if (Time.time > nextShotTime){
        
          if (fireMode == FireMode.Burst){
             
            if(shotsRemainingInBurst == 0){
              return;
            }
            shotsRemainingInBurst --;
          }
 
        else if (fireMode == FireMode.Single){
          
          if (!triggerRelaesedSinceLastShot){
            return;
          }
        }
        
        for (int i =0; i < _projectileSpawn.Length; i++){

        nextShotTime = Time.time + _msBetweenShots / 1000;
        GameObject newProjectile = Instantiate(_projectile, _projectileSpawn[i].position, _projectileSpawn[i].rotation);  
          newProjectile.TryGetComponent<Bullet>(out Bullet bullet);
          if (bullet != null){
               bullet.SetSpeed(_muzzleVelocity);
          }  

        }
        }
    }

    public void OnTriggerHold() {
      Shoot();
      triggerRelaesedSinceLastShot = false; 
    }

    public void OnTriggerrRelease() {
      triggerRelaesedSinceLastShot = true;
      shotsRemainingInBurst =  _burstCount;
    }

}

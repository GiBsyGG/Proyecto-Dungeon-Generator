using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{   
    [SerializeField]
    private Transform WeaponHold;
    [SerializeField]
    private Gun startingGun;
    public Gun equippedGun;
    
    void Start(){
        if (startingGun != null){
            EquipGun(startingGun);
        }
    }

    public void EquipGun(Gun gunToEquip){
        if (equippedGun != null) {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip,WeaponHold.position,WeaponHold.rotation) as Gun;
        equippedGun.transform.parent =WeaponHold;
    }
    
    public void OnTriggerHold(){
        if (equippedGun != null) {
            equippedGun.OnTriggerHold();
        }
    }

    public void OnTriggerrRelease(){
         if (equippedGun != null) {
            equippedGun.OnTriggerrRelease();
        }

    }
    
}

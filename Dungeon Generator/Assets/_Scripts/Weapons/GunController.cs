using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{   
    [SerializeField]
    private Transform WeaponHold;
    [SerializeField]
    private Gun[] allGuns;
    public Gun equippedGun;
    
    void Start(){
        if (allGuns.Length > 0){
            EquipGun(0);
        }
    }

    public void EquipGun(int gunIndex){
        if (equippedGun != null) {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(allGuns[gunIndex],WeaponHold.position,WeaponHold.rotation) as Gun;
        equippedGun.transform.parent =WeaponHold;

        // Comunicar cambio de arma para la interfaz
        //Debug.Log(equippedGun.UIGunIcon);
        GameEvents.OnGunChangeEvent?.Invoke(equippedGun.UIGunIcon);

    }

     public void EquipGun(Gun gunToEquip){
        if (equippedGun != null) {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip,WeaponHold.position,WeaponHold.rotation) as Gun;
        equippedGun.transform.parent =WeaponHold;

        // Comunicar cambio de arma para la interfaz
        GameEvents.OnGunChangeEvent?.Invoke(equippedGun.UIGunIcon);
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
    
    public void EquipLoot(Gun.GunType gunType)
    {
          foreach (Gun gun in allGuns)
          {
               if(gun.type == gunType)
               {
                    EquipGun(gun);
               }
          }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGunEquiped : MonoBehaviour
{
     public Image gunEquipedSprite;

     private void Start()
     {
          // Suscribimos el evento de cambio de arma para cambiar el sprite en la interfaz
          GameEvents.OnGunChangeEvent += OnWeaponUpdate;
     }

     private void OnDestroy()
     {
          GameEvents.OnGunChangeEvent -= OnWeaponUpdate;
     }

     private void OnWeaponUpdate(Sprite icon)
     {
          if(gunEquipedSprite != null)
          {
               gunEquipedSprite.sprite = icon;
          }
     }
}

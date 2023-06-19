using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGunEquiped : MonoBehaviour
{
     public Image gunEquipedSprite;

     private void OnWeaponUpdate(Sprite icon)
     {
          gunEquipedSprite.sprite = icon;
     }
}
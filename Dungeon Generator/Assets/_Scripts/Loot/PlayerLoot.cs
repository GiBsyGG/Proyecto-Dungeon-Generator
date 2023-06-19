using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLoot : MonoBehaviour
{
     private Player _player;
     private GunController _gunController;

     private void Start()
     {
          _player = GetComponent<Player>();
          _gunController = GetComponent<GunController>();
     }
     public void CollectLoot(Loot loot)
     {
          if (loot.type == LootType.Health)
          {
               HealthLoot healthLoot = (HealthLoot)loot;
               AddLifePoints(healthLoot.Amount);
          }
          else if (loot.type == LootType.Weapon)
          {
               WeaponLoot weaponLoot = (WeaponLoot)loot;
               ChangeGun(weaponLoot.gunType);
          }
     }

     public void AddLifePoints(int healthLootAmount)
     {
          if (_player != null)
          {
               if (_player.HealthPoints + healthLootAmount <= _player.TotalHealthPoints)
               {
                    _player.HealthPoints += healthLootAmount;
               }
               else
               {
                    _player.HealthPoints = _player.TotalHealthPoints;
               }
          }
     }

     public void ChangeGun(Gun.GunType gunType)
     {
          if(_gunController != null)
          {
               _gunController.EquipLoot(gunType);
          }
     }
}

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
          else if(loot.type == LootType.Key)
          {
               KeyLoot keyLoot = (KeyLoot)loot;
               keyObtainded();

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

          // Llamar al evento de la actualizar la vida
          GameEvents.OnPlayerHealthChangeEvent?.Invoke(_player.HealthPoints);

          AudioManager.Instance.PlaySound2D("PlayerHealed");
     }

     public void ChangeGun(Gun.GunType gunType)
     {
          if(_gunController != null)
          {
               _gunController.EquipLoot(gunType);

               AudioManager.Instance.PlaySound2D("InteractWeaponLoot");
          }
     }

     public void keyObtainded()
     {
          if (_player != null)
          {
               _player.HaveKey = true;

               // Comunicamos que obtuvo la llave
               GameEvents.OnPlayerKeyChangeEvent?.Invoke(_player.HaveKey);

               AudioManager.Instance.PlaySound2D("InteractKeyLoot");
          }
     }
}

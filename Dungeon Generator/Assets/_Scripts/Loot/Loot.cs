using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootType { Weapon, Health }
public class Loot : MonoBehaviour, IInteractable
{
     public LootType type;

     public virtual void Interact(PlayerInteractable player)
     {
          if (player.TryGetComponent(out PlayerLoot playerLoot))
               playerLoot.CollectLoot(this);

          // No sabemos si esto llama al ontrigger exit en el playerInteractable, por si acaso lo removemos
          // RemoveInteractable(this);
          Destroy(gameObject);
     }

}


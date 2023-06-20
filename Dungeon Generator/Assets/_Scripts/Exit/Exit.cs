using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour, IInteractable
{
     [SerializeField]
     private AbstractDungeonGenerator generator;

     private bool isUsed = false;

     public void NextDungeon()
     {
          // Para solo poder usarla una vez por mazmorra
          if (!isUsed)
          {
               // Evento para indicar el paso al siguiente dungeon? De momento no
               GameManager.Instance.DungeonStart();
          }
     }

     public virtual void Interact(PlayerInteractable player)
     {
          NextDungeon();
          isUsed = true;
     }

     public void RestoreExit()
     {
          isUsed = false;
     }
}

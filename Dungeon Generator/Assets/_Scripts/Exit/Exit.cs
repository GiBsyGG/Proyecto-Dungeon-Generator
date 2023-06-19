using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour, IInteractable
{
     public void NextDungeon()
     {
          // Evento para indicar el paso al siguiente dungeon?
          GameManager.Instance.HandleMenu();

     }

     public virtual void Interact(PlayerInteractable player)
     {
          NextDungeon();
     }
}

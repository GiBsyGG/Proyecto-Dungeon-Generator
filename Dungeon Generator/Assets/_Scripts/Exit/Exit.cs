using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour, IInteractable
{
     [SerializeField]
     private AbstractDungeonGenerator generator;

     public void NextDungeon()
     {
          // Evento para indicar el paso al siguiente dungeon?
          GameManager.Instance.HandleNextDungeon();

     }

     public virtual void Interact(PlayerInteractable player)
     {
          NextDungeon();
     }
}

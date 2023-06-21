using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour, IInteractable
{
     [SerializeField]
     private SpriteRenderer _stairsSprite;

     private bool isUsed = false;

     public void NextDungeon()
     {
          // Evento para indicar el paso al siguiente dungeon? De momento no
          GameManager.Instance.DungeonStart();
          
     }

     public virtual void Interact(PlayerInteractable player)
     {
          // Para solo poder usarla una vez por mazmorra
          if (!isUsed)
          {
               // Comprobamos que se tenga la llave
               if (GameManager.Instance.player.HaveKey)
               {
                    StartCoroutine(OpenHatchway());
                    // Indicamos que ya se interactuo con ella
                    isUsed = true;
               }
               else
               {
                    GameEvents.OnActiveMessageEvent?.Invoke(MessageType.NoKey);
               }
               
          }

     }

     public void RestoreExit()
     {
          isUsed = false;
     }

     IEnumerator OpenHatchway()
     {
          // Para una pequeña animacion abriendo la escotilla
          _stairsSprite.sortingOrder = 3;

          while (true)
          {
               yield return new WaitForSeconds(1f);
               break;
          }

          _stairsSprite.sortingOrder = 1;

          NextDungeon();
     }
}

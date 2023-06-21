using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour, IInteractable
{
     [SerializeField]
     Loot[] _loots;
     [SerializeField]
     private int _lootAmountDropped = 1;
     [SerializeField]
     private float _lootDuration = 5f;
     [SerializeField]
     private bool _disapearLoot = true;
     [SerializeField]
     private Sprite chestOpenSprite;
  
     private bool open = false;
     private Loot _lootDropped;


     // Cuando el player interaccione debe abrirse
     public void Open()
     {
          // Soltar los varios loots y destruirlos despues de un tiempo
          for (int i = 0; i < _lootAmountDropped; i++)
          {
               int randomLoot = Random.Range(0, _loots.Length);
               
               _lootDropped = Instantiate(_loots[randomLoot], transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity);

               // Inicia lo corrutina para desaparecer el loot si se debe hacer
               if(_disapearLoot)
                    StartCoroutine(DestroyLootAfterTime(_lootDropped));
          }

          // Luego de soltar el loot

          // Desactivo la animacion y cambio el sprite
          if (TryGetComponent(out Animator anim))
               anim.enabled = false;
          if (TryGetComponent(out SpriteRenderer spriteRenderer))
               spriteRenderer.sprite = chestOpenSprite;

          //Se apaga el collider para evitar ser detectado como interactuable
          if (TryGetComponent(out Collider2D collider))
               collider.enabled = false;

          // Se remueve de los interactuables para evitar ser detectado como interactuable
          //PlayerInteractable.Instance.RemoveInteractable(this);

          open = true;
     }

     public virtual void Interact(PlayerInteractable player)
     {
          // No sabemos si esto llama al ontrigger exit en el playerInteractable, por si acaso lo removemos
          // RemoveInteractable(this);
          // Para evitar que se abra varias veces
          if (!open)
          {
               Open();
          }
     }

     // Corrutina para desaparecer el Loot despues de un tiempo
     IEnumerator DestroyLootAfterTime(Loot loot)
     {
          while (true)
          {
               // Esperar 5 segundos
               yield return new WaitForSeconds(_lootDuration);
               break;
          }

          // Comprobamos que el loot no se haya destruido al ser usado por el player
          if(loot != null)
          {
               // Destruir el objeto despues de esperar los segundos
               Destroy(loot.gameObject);
          }
          
     }

}

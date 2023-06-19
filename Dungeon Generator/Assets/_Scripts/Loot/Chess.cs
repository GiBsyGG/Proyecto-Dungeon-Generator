using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour, IInteractable
{
     [SerializeField]
     Loot[] _loots;
     [SerializeField]
     private Sprite chestOpenSprite;
     private bool open = false;


     // Cuando el player interaccione debe abrirse
     public void Open()
     {
          int randomLoot = Random.Range(0, _loots.Length);
          // TODO: Instanciar el loot en un area cerca y con rotacion random
          Instantiate(_loots[randomLoot], transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity);

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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
     [SerializeField]
     private GameObject _interactionIcon;

     public static PlayerInteractable Instance;

    private List<IInteractable> _objectsInRange = new();

    public bool HasObjectsInRange => _objectsInRange.Count > 0;
     // public IInteractable ClosestInteractable => _objectsInRange[0];

     private void Awake()
     {
          // If there is an instance, and it's not me, delete myself.
          if (Instance != null && Instance != this)
          {
               Destroy(this);
          }
          else
          {
               Instance = this;
          }
     }

     private void Update()
     {
          if (_objectsInRange.Count > 0)
          {
               _interactionIcon.SetActive(true);
               // Lanzar el evento de posible interaccion?
          }
          else
          {
               _interactionIcon.SetActive(false);
          }

          if (Input.GetKeyDown(KeyCode.T))
          {
               ActiveInteraction();
          }

     }

     private void ActiveInteraction()
     {
          if(_objectsInRange.Count == 0)
               return;

          // Buscar el más cercano? en nuestro caso manejamos poco entonces el primero
          _objectsInRange[0].Interact(this);
     }

     // En caso de que el onttriger no funcione al recoger el loot por ejemplo nos aseguramos con esto
     public void RemoveInteractable(IInteractable interactable)
     {
          _objectsInRange.Remove(interactable);
     }

     // En caso de que al cambiar la posicion del player no funcione nos aseguramos con esto
     public void ClearObjects()
     {
          _objectsInRange.Clear();
     }

     private void OnTriggerEnter2D(Collider2D other)
     {
          if(other.transform.TryGetComponent(out IInteractable obj))
          {
               _objectsInRange.Add(obj);
          }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
          if(other.transform.TryGetComponent(out IInteractable obj))
          {
               _objectsInRange.Remove(obj);

          }
     }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPlayerHealth : MonoBehaviour
{
     private List<Image> _hearts = new List<Image>();
     
    // Start is called before the first frame update
    void Start()
    {
          int childCount = transform.childCount;
          for (int i = 0; i < childCount; i++)
          {
               Transform child = transform.GetChild(i);
               if (child != null)
               {
                    if(child.TryGetComponent<Image>(out Image image))
                         _hearts.Add(image);
               }
          }

          // Suscribimos el evento
          GameEvents.OnPlayerHealthChangeEvent += OnPlayerHealthChange;
    }

     private void OnDestroy()
     {
          // Desuscribimos el evento
          GameEvents.OnPlayerHealthChangeEvent -= OnPlayerHealthChange;
     }

     private void OnPlayerHealthChange(int health)
     {
          // Se pintara un corazon en la lista o no, segun la vida del player
          for (int i = 0; i < _hearts.Count; i++)
          {
               if(i < health)
               {
                    _hearts[i].gameObject.SetActive(true);
               }
               else
               {
                    _hearts[i].gameObject.SetActive(false);
               }
          }
     }
}

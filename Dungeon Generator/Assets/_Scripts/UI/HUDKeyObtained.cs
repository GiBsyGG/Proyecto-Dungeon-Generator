using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDKeyObtained : MonoBehaviour
{
     private Image _key;

    // Start is called before the first frame update
    void Start()
    {
          Transform child = transform.GetChild(0);
          if (child.TryGetComponent<Image>(out Image image))
               _key = image;
               


          GameEvents.OnPlayerKeyChangeEvent += OnShowKeyChange;
    }

     private void OnDestroy()
     {
          GameEvents.OnPlayerKeyChangeEvent -= OnShowKeyChange;
     }

     public void OnShowKeyChange(bool showKey)
     {
          if(_key != null)
          {
               if (showKey)
               {
                    _key.color = Color.white;
               }
               else
               {
                    _key.color = new Color(0.25f,0.25f,0.25f);
               }
          }
     }
}

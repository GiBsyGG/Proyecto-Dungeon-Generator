using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MessageType { NoKey };

public class HUDMessages : MonoBehaviour
{
     [SerializeField]
     private TextMeshProUGUI _message;

     [SerializeField]
     private GameObject _messageContainer;

     private bool _messageOnScreen = false;

    // Start is called before the first frame update
    void Start()
    {
          if(_messageContainer != null )
               _messageContainer.SetActive(false);
          GameEvents.OnActiveMessageEvent += OnShowMessage;
    }

     private void OnDestroy()
     {
          GameEvents.OnActiveMessageEvent -= OnShowMessage;
     }

     public void OnShowMessage(MessageType messageType)
     {
          if( _message != null)
          {
               switch (messageType)
               {
                    case MessageType.NoKey:
                         _message.text = "No tienes la llave";
                         break;
                    default:
                         break;
               }
          }

          if (!_messageOnScreen)
          {
               StartCoroutine(ShowMessageOnScreen());
          }          
     }
    
     IEnumerator ShowMessageOnScreen()
     {
          if (_messageContainer != null)
               _messageContainer.SetActive(true);

          _messageOnScreen = true;

          while (true)
          {
               yield return new WaitForSeconds(3f);
               break;
          }

          if (_messageContainer != null)
               _messageContainer.SetActive(false);

          _messageOnScreen = false;
     }
}

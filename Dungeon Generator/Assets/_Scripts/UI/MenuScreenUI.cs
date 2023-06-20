using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenUI : MonoBehaviour
{
     [SerializeField]
     private Button _startButton;

     private void Start()
     {
          _startButton.onClick.AddListener(OnStartButtonClicked);
     }

     public void OnStartButtonClicked()
     {
          // Para evitar mas interacciones con el boton, por ahora queremos varias interacciones
          //_startButton.interactable = false;

          GameManager.Instance.GameStart();
     }
}

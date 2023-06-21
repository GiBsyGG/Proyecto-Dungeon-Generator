using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenUI : MonoBehaviour
{
     [SerializeField]
     private Button _startButton;
     [SerializeField]
     private Button _controlsButton;
     [SerializeField]
     private GameObject _controlsPanel;

     private bool _controlsOnScreen = false;

     private void Start()
     {

          // Apagamos el panel de los controles por defecto
          _controlsPanel.SetActive(false);

          _startButton.onClick.AddListener(OnStartButtonClicked);
          _controlsButton.onClick.AddListener(OnToggleControls);
     }

     public void OnStartButtonClicked()
     {
          // Para evitar mas interacciones con el boton, por ahora queremos varias interacciones
          //_startButton.interactable = false;

          // Al iniciar el juego tambien apagamos los botones
          OffControlsPanel();

          GameManager.Instance.GameStart();
     }

     public void OnToggleControls()
     {
          if(_controlsPanel != null)
          {
               if (_controlsOnScreen)
               {
                    OffControlsPanel();
               }
               else
               {
                    ShowControlsPanel();
               }
          }
     }

     public void OffControlsPanel()
     {
          _controlsPanel.SetActive(false);
          _controlsOnScreen = false;
     }

     public void ShowControlsPanel()
     {
          _controlsPanel.SetActive(true);
          _controlsOnScreen = true;
     }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenUI : MonoBehaviour
{
     [SerializeField]
     private Button[] _startButton;
     [SerializeField]
     private Button[] _quitButton;
     [SerializeField]
     private Button[] _controlsButton;
     [SerializeField]
     private GameObject[] _controlsPanel;
     

     private bool _controlsOnScreen = false;

     private void Start()
     {
          for (int i = 0; i < _startButton.Length; i++)
          {    // Apagamos el panel de los controles por defecto
               _controlsPanel[i].SetActive(false);
               _startButton[i].onClick.AddListener(OnStartButtonClicked);
               _controlsButton[i].onClick.AddListener(OnToggleControls);
               _quitButton[i].onClick.AddListener(ExitGame);
          }
     }

     public void OnStartButtonClicked()
     {
          // Para evitar mas interacciones con el boton, por ahora queremos varias interacciones
          //_startButton.interactable = false;

          // Al iniciar el juego tambien apagamos los botones
          OffControlsPanel();

          GameManager.Instance.GameStart();

          AudioManager.Instance.PlaySound2D("ButtonPressed");
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

          AudioManager.Instance.PlaySound2D("ButtonPressed");
     }

     public void OffControlsPanel()
     {
          for (int i = 0; i < _controlsPanel.Length; i++)
          {    
               _controlsPanel[i].SetActive(false);
               _controlsOnScreen = false;
          }
     }

     public void ShowControlsPanel()
     {
          for (int i = 0; i < _controlsPanel.Length; i++)
          {
               _controlsPanel[i].SetActive(true);
               _controlsOnScreen = true;
          }
     }

     public void ExitGame()
     {
          Application.Quit();
     }
}

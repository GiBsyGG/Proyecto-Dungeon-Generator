using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadScreenUI : MonoBehaviour
{
     [SerializeField]
     private Button _backToMenuButton;
     [SerializeField]
     private TextMeshProUGUI _dungeonLevelText;

     private void Start()
     {
          _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);

          GameEvents.OnPlayerDeathEvent += OnDeathPlayer;
     }

     private void OnDestroy()
     {
          GameEvents.OnPlayerDeathEvent -= OnDeathPlayer;
     }

     public void OnBackToMenuButtonClicked()
     {
          // Para evitar mas interacciones con el boton, en este caso es mejor no desactivarlo
          //_backToMenuButton.interactable = false;

          GameManager.Instance.BackToMenu();
     }

     public void OnDeathPlayer(int dungeonLevel)
     {
          _dungeonLevelText.text = $"Moriste en el Dungeon: {dungeonLevel}";
     }
}

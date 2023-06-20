using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextDungeonScreenUI : MonoBehaviour
{
     [SerializeField]
     private TextMeshProUGUI _dungeonMessageText;

     [SerializeField]
     private TextMeshProUGUI _dungeonLevelText;

     private string[] messages = { 
          "�Eran zombies o estudiantes?", "Sobreviv�... por desgracia.",
          "Esto no acaba nunca, como mi carrera", "La pala es para enterrar mis sue�os",
          "Esta pantalla de carga es mentira, igual que sus palabras",
          "Ojal� hubiera motivaci�n looteable en los cofres"
          };

     void Start()
     {
          Debug.Log(_dungeonMessageText.text);
          GameEvents.OnChangeDungeonEvent += OnStartNewDungeon;
     }

     private void OnDestroy()
     {
          GameEvents.OnChangeDungeonEvent -= OnStartNewDungeon;
     }

     private void OnStartNewDungeon(int dungeonLevel)
     {
          Debug.Log(dungeonLevel);
          _dungeonMessageText.text = messages[Random.Range(0, messages.Length)];
          _dungeonLevelText.text = $"Siguiente Dungeon: {dungeonLevel}";
     }
}

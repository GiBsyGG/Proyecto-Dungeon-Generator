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
          "Recuerda revisar los cofres al acabar con los zombies", "A veces las salidas son las menos obvias",
          "Estamos en un ciclo infinito", "Recuerda que tienes atacas a melee",
          "Entre mas intentes escapar de tu pasado, mas tendras que bajar",
          "Por que caimos aqui?", "La pala es para enterrar mis esperanzas",
          "Busca la llave o no saldras"
          };

     void Start()
     {
          GameEvents.OnChangeDungeonEvent += OnStartNewDungeon;
     }

     private void OnDestroy()
     {
          GameEvents.OnChangeDungeonEvent -= OnStartNewDungeon;
     }

     private void OnStartNewDungeon(int dungeonLevel)
     {
          _dungeonMessageText.text = messages[Random.Range(0, messages.Length)];
          _dungeonLevelText.text = $"Siguiente Dungeon: {dungeonLevel}";
     }
}

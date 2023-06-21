using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDDungeonLevel : MonoBehaviour
{
     [SerializeField]
     private TextMeshProUGUI _dungeonLevelText;

     // Start is called before the first frame update
     void Start()
    {
          GameEvents.OnChangeDungeonEvent += OnUpdateDungeonLevelText;
    }

     private void OnDestroy()
     {
          GameEvents.OnChangeDungeonEvent -= OnUpdateDungeonLevelText;
     }

     public void OnUpdateDungeonLevelText(int dungeonLevel)
     {
          if (_dungeonLevelText != null)
               _dungeonLevelText.text= dungeonLevel.ToString();
     }
}

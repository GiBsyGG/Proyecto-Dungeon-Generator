using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class PlayerGeneration
{
     public static void GeneratePlayer(Vector2Int initialPosition)
     {
          Player player = GameManager.Instance.player;
          if (player != null)
          {
               player.OnRevive();
               
               player.gameObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, 0);
          }
     }
}

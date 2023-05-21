using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class PlayerGeneration
{
     public static void GeneratePlayer(Vector2Int initialPosition)
     {
          // Acceder al objeto player en la jerarquía
          GameObject playerObject = GameObject.Find("Player");
          if (playerObject != null)
          {
               playerObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, 0);
          }
     }
}

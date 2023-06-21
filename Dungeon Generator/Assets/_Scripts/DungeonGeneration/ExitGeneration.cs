using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExitGeneration
{
     public static void GenerateExit(Vector2Int initialPosition)
     {
          Exit exit = GameManager.Instance.exit;
          if (exit != null)
          {
               exit.RestoreExit();
               exit.transform.position = new Vector3(initialPosition.x, initialPosition.y, 0);
          }
     }
}

public class RoomGoal
{
     private Vector2Int _roomCenter;
     private HashSet<Vector2Int> _roomFloor;

     // Metodo para simular el start
     public RoomGoal(Vector2Int center, HashSet<Vector2Int> floor)
     {
          _roomCenter = center;
          _roomFloor = floor;
     }
}

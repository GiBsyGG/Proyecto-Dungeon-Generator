using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomDecorationGenerator
{
     public static void CreateRoomDecoration(HashSet<Vector2Int> floorPositions, List<Vector2Int> roomCenters, TilemapVisualizer tilemapVisualizer)
     {
          var potencialPositions = FindPotentialPositions(floorPositions, roomCenters, Direction2D.eightDirectionsList);

          DecorateRoom(tilemapVisualizer, potencialPositions, floorPositions);
     }

     private static HashSet<Vector2Int> FindPotentialPositions(HashSet<Vector2Int> floorPositions, List<Vector2Int> roomCenters, List<Vector2Int> eightDirectionsList)
     {
          HashSet<Vector2Int> potentialPositions = new HashSet<Vector2Int>();

          return potentialPositions;
     }

     private static void DecorateRoom(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> potencialPositions, HashSet<Vector2Int> floorPositions)
     {
          throw new NotImplementedException();
     }

}

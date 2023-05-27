using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyGenerator
{
     public static List<GameObject> GenerateEnemys(HashSet<Vector2Int> floorPositions, Vector2Int roomCenter, GameObject enemy, float amount)
     {
          var potencialPositions = FindPotentialPositions(floorPositions, roomCenter, amount, Direction2D.eightDirectionsList);

          return generate(potencialPositions, enemy);
     }

     private static HashSet<Vector2Int> FindPotentialPositions(HashSet<Vector2Int> floorPositions, Vector2Int roomCenters, float amount, List<Vector2Int> eightDirectionsList)
     {
          HashSet<Vector2Int> potentialPositions = new HashSet<Vector2Int>();

          foreach (var position in floorPositions)
          {
               if (UnityEngine.Random.Range(0f, 1f) < amount)
               {
                    potentialPositions.Add(position);
               }
          }

          return potentialPositions;
     }

     private static List<GameObject> generate(HashSet<Vector2Int> potencialPositions, GameObject enemy)
     {
          List<GameObject> enemies = new List<GameObject>();
          foreach (var position in potencialPositions)
          {
               GameObject objectEnemy = GameObject.Instantiate(enemy);
               objectEnemy.transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);

               enemies.Add(objectEnemy);
          }

          return enemies;
     }
}

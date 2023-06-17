using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyGenerator : MonoBehaviour
{

     public static List<GameObject> GenerateEnemys(HashSet<Vector2Int> floorPositions, Vector2Int roomCenter, GameObject enemy, float amount)
     {
          var potencialPositions = FindPotentialPositions(floorPositions, roomCenter, amount, Direction2D.eightDirectionsList);

          return generate(potencialPositions, enemy);
     }

     private static HashSet<Vector2Int> FindPotentialPositions(HashSet<Vector2Int> floorPositions, Vector2Int roomCenters, float amount, List<Vector2Int> eightDirectionsList)
     {
          HashSet<Vector2Int> potentialPositions = new HashSet<Vector2Int>();
          // TODO: Cambiar criterio para posiciones potenciales, usando overlapBox para saber si se est� cerca de un muro u otro enemigo
          // Otra forma es filtrar posiciones para conocer si hay dos muy cerca
          // Otra es buscar comenzando desde el centro
          foreach (var position in floorPositions)
          {
               if (UnityEngine.Random.Range(0f, 1f) < amount/4)
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

     public static void DestroyEnemies(List<GameObject> enemies)
     {
          Debug.Log("Enemigos: ");
          foreach(var enemy in enemies)
          {
               Destroy(enemy);
          }
          enemies.Clear();
     }
}

public class RoomCombat
{
     private List<GameObject> _enemies;

     // Metodo para simular el start
     public void Start()
     {
          _enemies = new List<GameObject>();
          // Suscribir el evento de muerte de cada enemigo
          GameEvents.OnEnemyDeath += OnEnemyDeath;

     }

     // Metodo para simular el destroy
     public void OnDestroy()
     {
          _enemies.Clear();
          GameEvents.OnEnemyDeath -= OnEnemyDeath;
     }

     public void SetEnemies(List<GameObject> enemies)
     {
          _enemies = enemies;
     }

     public void DeleteEnemies()
     {
          foreach (var enemy in _enemies)
          {
               MonoBehaviour.Destroy(enemy);
          }
          _enemies.Clear();
     }

     public void OnEnemyDeath(Enemy deathEnemy)
     {
          // Que el enemigo que murio si sea el de esa sala
          if (_enemies.Contains(deathEnemy.gameObject))
          {
               if (_enemies.Count > 0)
               {
                    
                    _enemies.Remove(deathEnemy.gameObject);
               }
               if (_enemies.Count == 0)
               {
                    // TODO: Al morir todos se instancia un cofre, este con su  l�gica interna, tal vez un prefab
                    // Tal vez aqu� solo sea crear la instancia
                    Debug.Log("Todos muertos");
               }
          }
     }
}

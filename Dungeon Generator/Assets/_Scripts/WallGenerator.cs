using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
     /// <summary>
     /// Responsable de colocar walls en el Tilemap
     /// </summary>
     /// <param name="floorPositions"> HashSet con las posiciones del suelo en el Tilemap </param>
     /// <param name="tilemapVisualizer"> Tilemap al que se le colocarán walls alrededor </param>
     public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
     {
          var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);

          // Pintaremos el muro en cada posicion
          foreach (var position in basicWallPositions)
          {
               tilemapVisualizer.PaintSingleBasicWall(position);
          }
     }


     /// <summary>
     /// Encuentra las posiciones de los muros del Tilemap basandose en direcciones vecinas de las posiciones del floor
     /// </summary>
     /// <param name="floorPositions"> Posiciones del floor del Tilemap </param>
     /// <param name="directionsList"> Lista de direcciones cardinales usada para hallar las 4 direcciones vecinas </param>
     /// <returns> HashSet Con las posiciones de los muros en el Tilemap </returns>
     private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
     {
          HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

          foreach (var position in floorPositions)
          {
               // Para cada posición del suelo miraremos las posiciones en las direcciones cardinales o sea sus vecinos alrededor
               foreach (var direction in directionsList)
               {
                    var neighbourPosition = position + direction;
                    // Si el vecino no hace parte del suelo, indica que es un muro (solo funciona en direcciones cardinales, no en esquinas)
                    if(floorPositions.Contains(neighbourPosition) == false)
                    {
                         wallPositions.Add(neighbourPosition);
                    }
               }
          }

          return wallPositions;
     }
}

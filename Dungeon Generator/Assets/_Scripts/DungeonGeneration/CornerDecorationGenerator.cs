using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CornerDecorationGenerator
{
     /// <summary>
     /// Responsable de colocar la corner decoration en el Tilemap
     /// </summary>
     /// <param name="floorPositions"> HashSet con las posiciones del suelo en el Tilemap </param>
     /// <param name="tilemapVisualizer"> Tilemap al que se le colocarán walls alrededor </param>
     public static void CreateCornerDecoration(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
     {
          var cornerPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);

          DecorateCorner(tilemapVisualizer, cornerPositions, floorPositions);
     }

     /// <summary>
     /// Encuentra las posiciones de los muros del Tilemap basandose en direcciones vecinas de las posiciones del floor
     /// </summary>
     /// <param name="floorPositions"> Posiciones del floor del Tilemap </param>
     /// <param name="directionsList"> Lista de direcciones cardinales usada para hallar las 4 direcciones vecinas </param>
     /// <returns> HashSet Con las posiciones de los muros en el Tilemap </returns>
     private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
     {
          HashSet<Vector2Int> cornerPositions = new HashSet<Vector2Int>();

          foreach (var position in floorPositions)
          {
               // Para cada posición del suelo miraremos las posiciones en las direcciones cardinales o sea sus vecinos alrededor
               foreach (var direction in directionsList)
               {
                    var neighbourPosition = position + direction;
                    // Si el vecino no hace parte del suelo, indica que es un muro (solo funciona en direcciones cardinales, no en esquinas)
                    if (floorPositions.Contains(neighbourPosition) == false)
                    {
                         cornerPositions.Add(neighbourPosition);
                    }
               }
          }

          return cornerPositions;
     }


     /// <summary>
     /// Método encargado de crear las esquinas y corregir los muros básicos pintados de manera equivocada
     /// </summary>
     /// <param name="tilemapVisualizer"> Tilemap donde se pintarán los muros </param>
     /// <param name="cornerWallPositions"> Posición de la esquina del muro </param>
     /// <param name="floorPositions"> Posiciones del suelo </param>
     private static void DecorateCorner(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
     {
          foreach (var position in cornerWallPositions)
          {
               // Ubicamos los vecinos para asignarles el correspondiente valor binario de muro para esta posición
               // Aquí al ser esquina revisamos las 8 posibles direcciones para saber que tipo de esquina
               String neighboursBinaryType = "";
               foreach (var direction in Direction2D.eightDirectionsList)
               {
                    var neighboursPosition = position + direction;
                    if (floorPositions.Contains(neighboursPosition))
                    {
                         neighboursBinaryType += "1";
                    }
                    else
                    {
                         neighboursBinaryType += "0";
                    }
               }
               tilemapVisualizer.PaintSingleCornerDecoration(position, neighboursBinaryType);
          }
     }

}

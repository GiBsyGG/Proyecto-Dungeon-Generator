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
          var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);

          // Pintaremos el muro en cada posicion
          CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);
          CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
     }


     /// <summary>
     /// Método encargado de crear las esquinas y corregir los muros básicos pintados de manera equivocada
     /// </summary>
     /// <param name="tilemapVisualizer"> Tilemap donde se pintarán los muros </param>
     /// <param name="cornerWallPositions"> Posición de la esquina del muro </param>
     /// <param name="floorPositions"> Posiciones del suelo </param>
     private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
     {
          foreach (var position in cornerWallPositions)
          {
               // Ubicamos los vecinos para asignarles el correspondiente valor binario de muro para esta posición
               // Aquí al ser esquina revisamos las 8 posibles direcciones para saber que tipo de esquina
               String neighboursBinaryType = "";
               foreach (var direction in Direction2D.eightDirectionsList)
               {
                    var neighboursPosition = position + direction;
                    if(floorPositions.Contains(neighboursPosition))
                    {
                         neighboursBinaryType += "1";
                    }
                    else
                    {
                         neighboursBinaryType += "0";
                    }
               }
               tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
          }
     }


     /// <summary>
     /// Método encargado de crear y pintar los muros básicos
     /// </summary>
     /// <param name="tilemapVisualizer"> Tilemap donde se pintarán los muros </param>
     /// <param name="basicWallPositions"> Posición cardinal del del muro </param>
     /// <param name="floorPositions"> Posiciones del suelo </param>
     private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
     {
          foreach (var position in basicWallPositions)
          {
               // Ubicamos los vecinos para asignarles el correspondiente valor binario de muro para esta posición
               String neighboursBinaryType = "";
               foreach (var direction in Direction2D.cardinalDirectionsList)
               {
                    var neighboursPosition = position + direction;
                    if(floorPositions.Contains(neighboursPosition))
                    {
                         neighboursBinaryType += "1";
                    }
                    else
                    {
                         neighboursBinaryType += "0";
                    }
               }
               tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
     /// <summary>
     /// Genera el algoritmo para recorrer diferentes posiciones aleatoriamente
     /// </summary>
     /// <param name="startPosition"> La posición donde iniciamos la caminata </param>
     /// <param name="walkLength"> cuantos pasos hará nuestro agente antes de detenerse y devolver nuestros valores </param>
     /// <returns></returns>
     public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
     {
          HashSet<Vector2Int> path = new HashSet<Vector2Int>();

          path.Add(startPosition);
          var previousPosition = startPosition;

          for (int i = 0; i < walkLength; i++)
          {
               var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
               path.Add(newPosition);
               previousPosition = newPosition;
          }
          return path;

     }

     /// <summary>
     /// Hará al RandomWalk seleccionar una sola dirección y en esta dirección caminará a travez de la longitud del corredor 
     /// </summary>
     /// <param name="startPostion"> posición donde inicia el recorrido </param>
     /// <param name="corridorLength"> cantidad de pasos que se daran en el recorrido para crear el corredor </param>
     /// <returns> Retorna el List del camino creado para el corredor </returns>
     public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPostion, int corridorLength)
     {
          List<Vector2Int> corridor  = new List<Vector2Int>();
          var direction = Direction2D.GetRandomCardinalDirection();
          var currentPosition = startPostion;
          // Importante añadir la posición inicial
          corridor.Add(currentPosition);

          for (int i = 0; i < corridorLength; i++)
          {
               currentPosition += direction;
               corridor.Add(currentPosition);
          }

          return corridor;
     }
}

/// <summary>
/// Permite obtener una direccion random
/// </summary>
public static class Direction2D
{
     public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
     {
          new Vector2Int(0,1), //UP
          new Vector2Int(1,0), //RIGHT
          new Vector2Int(0,-1), //DOWN
          new Vector2Int(-1, 0) //LEFT
     };

     // Creamos un método para la dirección randmo
     public static Vector2Int GetRandomCardinalDirection()
     {
          return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
     }
}
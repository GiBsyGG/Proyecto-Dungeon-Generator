using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Rnd = System.Random;

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
     /// Genera el algoritmo para recorrer diferentes posiciones aleatoriamente
     /// </summary>
     /// <param name="startPosition"> La posición donde iniciamos la caminata </param>
     /// <param name="walkLength"> cuantos pasos hará nuestro agente antes de detenerse y devolver nuestros valores </param>
     /// <param name="rnd"> Random con semilla para controlar el valor de los aleatorios </param>
     /// <returns> Lista con las posiciones del camino generado </returns>
     public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength, Rnd rnd)
     {
          HashSet<Vector2Int> path = new HashSet<Vector2Int>();

          path.Add(startPosition);
          var previousPosition = startPosition;

          for (int i = 0; i < walkLength; i++)
          {
               var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection(rnd);
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


     /// <summary>
     /// Hará al RandomWalk seleccionar una sola dirección y en esta dirección caminará a travez de la longitud del corredor 
     /// </summary>
     /// <param name="startPostion"> posición donde inicia el recorrido </param>
     /// <param name="corridorLength"> cantidad de pasos que se daran en el recorrido para crear el corredor </param>
     /// <param name="seed"> Random con semilla para controlar el valor de los aleatorios </param>
     /// <returns> Lista con las posiciones del corredor </returns>
     public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPostion, int corridorLength, Rnd rnd)
     {
          List<Vector2Int> corridor = new List<Vector2Int>();
          var direction = Direction2D.GetRandomCardinalDirection(rnd);
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


     /// <summary>
     /// Dividirá un espacio en las diferentes rooms de manera aleatoria
     /// </summary>
     /// <param name="spaceToSplit"> Espacio que se dividirá en rooms </param>
     /// <param name="minWidth"> anchura minima del room </param>
     /// <param name="minHeight"> altura minima del room </param>
     /// <returns> Una List con las rooms resultantes de la división </returns>
     public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
     {
          // Room que podremos tomar y si es posible dividir
          Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();

          // Rooms resultantes de la división
          List<BoundsInt> roomsList = new List<BoundsInt>();

          // Ponemos primero en cola el espacio a dividir
          roomsQueue.Enqueue(spaceToSplit);

          // Mientras tengamos salas para dividir trataremos de hacerlo
          while (roomsQueue.Count > 0)
          {
               // Obtenemos las room para dividir
               var room = roomsQueue.Dequeue();

               // Descartamos rooms que son muy pequeñas para el room generation
               if(room.size.y >= minHeight && room.size.x >= minWidth)
               {
                    // De manera random decidimos si se divide vertical u horizontal
                    if(Random.value < 0.5f)
                    {
                         // Miramos si se puede dividir horizontalmente en dos rooms
                         if(room.size.y >= minHeight * 2)
                         {
                              SplitHorizontally(minHeight, roomsQueue, room);
                         } else if(room.size.x >= minWidth * 2)
                         {
                              SplitVertically(minWidth, roomsQueue, room);
                         } else if(room.size.x >= minWidth && room.size.y >= minHeight)
                         {
                              // El area no puede ser dividia pero puede contener un room, por tanto lo añadimos a la lista pero no a la cola
                              roomsList.Add(room);
                         }
                    }
                    else
                    {
                         // Miramos si se puede dividir verticalmente en dos rooms
                         if (room.size.x >= minWidth * 2)
                         {
                              SplitVertically(minWidth, roomsQueue, room);
                         } else if (room.size.y >= minHeight * 2)
                         {
                              SplitHorizontally(minHeight, roomsQueue, room);
                         }
                         else if (room.size.x >= minWidth && room.size.y >= minHeight)
                         {
                              // El area no puede ser dividia pero puede contener un room, por tanto lo añadimos a la lista pero no a la cola
                              roomsList.Add(room);
                         }
                    }
               }
          }

          return roomsList;
     }


     /// <summary>
     /// Dividirá un espacio en las diferentes rooms de manera aleatoria
     /// </summary>
     /// <param name="spaceToSplit"> Espacio que se dividirá en rooms </param>
     /// <param name="minWidth"> anchura minima del room </param>
     /// <param name="minHeight"> altura minima del room </param>
     /// <param name="seed"> Valor de la semill para generar el room </param>
     /// <returns> Una List con las rooms resultantes de la división </returns>
     public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight, int seed)
     {
          // Room que podremos tomar y si es posible dividir
          Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();

          // Rooms resultantes de la división
          List<BoundsInt> roomsList = new List<BoundsInt>();

          // Ponemos primero en cola el espacio a dividir
          roomsQueue.Enqueue(spaceToSplit);

          Rnd rnd = new Rnd(seed);

          // Mientras tengamos salas para dividir trataremos de hacerlo
          while (roomsQueue.Count > 0)
          {
               // Obtenemos las room para dividir
               var room = roomsQueue.Dequeue();

               // Descartamos rooms que son muy pequeñas para el room generation
               if (room.size.y >= minHeight && room.size.x >= minWidth)
               {
                    // De manera random decidimos si se divide vertical u horizontal
                    if (rnd.NextDouble() < 0.5f)
                    {
                         // Miramos si se puede dividir horizontalmente en dos rooms
                         if (room.size.y >= minHeight * 2)
                         {
                              SplitHorizontally(minHeight, roomsQueue, room, rnd);
                         }
                         else if (room.size.x >= minWidth * 2)
                         {
                              SplitVertically(minWidth, roomsQueue, room, rnd);
                         }
                         else if (room.size.x >= minWidth && room.size.y >= minHeight)
                         {
                              // El area no puede ser dividia pero puede contener un room, por tanto lo añadimos a la lista pero no a la cola
                              roomsList.Add(room);
                         }
                    }
                    else
                    {
                         // Miramos si se puede dividir verticalmente en dos rooms
                         if (room.size.x >= minWidth * 2)
                         {
                              SplitVertically(minWidth, roomsQueue, room, rnd);
                         }
                         else if (room.size.y >= minHeight * 2)
                         {
                              SplitHorizontally(minHeight, roomsQueue, room, rnd);
                         }
                         else if (room.size.x >= minWidth && room.size.y >= minHeight)
                         {
                              // El area no puede ser dividia pero puede contener un room, por tanto lo añadimos a la lista pero no a la cola
                              roomsList.Add(room);
                         }
                    }
               }
          }

          return roomsList;
     }


     /// <summary>
     /// Método para hacer divisiones de un espacio o room horizontalmente de manera aleatoria
     /// </summary>
     /// <param name="minHeight"></param>
     /// <param name="roomsQueue"> Cola de las rooms que se verifican para dividir </param>
     /// <param name="room"> Room que se dividirá en dos rooms </param>
     private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
     {
          // Lógica similar al split vertical 
          var ySplit = Random.Range(1, room.size.y);
          BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
          BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
               new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

          roomsQueue.Enqueue(room1);
          roomsQueue.Enqueue(room2);

     }


     /// <summary>
     /// Método para hacer divisiones de un espacio o room horizontalmente de manera aleatoria
     /// </summary>
     /// <param name="minHeight"></param>
     /// <param name="roomsQueue"> Cola de las rooms que se verifican para dividir </param>
     /// <param name="room"> Room que se dividirá en dos rooms </param>
     /// <param name="rnd"> Valor del aleatorio con semilla para controlar el espacio de la division </param>
     private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room, Rnd rnd)
     {
          // Lógica similar al split vertical 
          var ySplit = rnd.Next(1, room.size.y);
          BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
          BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
               new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

          roomsQueue.Enqueue(room1);
          roomsQueue.Enqueue(room2);

     }


     /// <summary>
     /// Método para hacer divisiones de un espacio o room verticalmente de manera aleatoria
     /// </summary>
     /// <param name="minWidth"></param>
     /// <param name="roomsQueue"> Cola de las rooms que se verifican para dividir </param>
     /// <param name="room"> Room que se dividirá en dos rooms </param>
     private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
     {
          // El size.x no toma el borde final
          var xSplit = Random.Range(1, room.size.x);  // Podemos usar el minHeight para ajustar dos rooms en una division pero queremos usar el factor random

          // Counstrimos nuestras cajas room 1 y room2 pasando la posición inicial y luego el tamaño
          BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));

          BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
               new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

          // Ahora que encontramos en sala las ponemos en la cola para ver si pueden ser divididas
          roomsQueue.Enqueue(room1);
          roomsQueue.Enqueue(room2);
     }


     /// <summary>
     /// Método para hacer divisiones de un espacio o room verticalmente de manera aleatoria
     /// </summary>
     /// <param name="minWidth"></param>
     /// <param name="roomsQueue"> Cola de las rooms que se verifican para dividir </param>
     /// <param name="room"> Room que se dividirá en dos rooms </param>
     /// <param name="rnd"> Valor del aleatorio con semilla para controlar el espacio de la division </param>
     private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room, Rnd rnd)
     {
          // El size.x no toma el borde final
          var xSplit = rnd.Next(1, room.size.x);  // Podemos usar el minHeight para ajustar dos rooms en una division pero queremos usar el factor random

          // Counstrimos nuestras cajas room 1 y room2 pasando la posición inicial y luego el tamaño
          BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));

          BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
               new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

          // Ahora que encontramos en sala las ponemos en la cola para ver si pueden ser divididas
          roomsQueue.Enqueue(room1);
          roomsQueue.Enqueue(room2);
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

     public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
     {
          new Vector2Int(1,1), //UP-RIGHT
          new Vector2Int(1,-1), //RIGHT-DOWN
          new Vector2Int(-1,-1), //DOWN-LEFT
          new Vector2Int(-1, 1) //LEFT-UP
     };

     public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
     {
          new Vector2Int(0,1), //UP
          new Vector2Int(1,1), //UP-RIGHT
          new Vector2Int(1,0), //RIGHT
          new Vector2Int(1,-1), //RIGHT-DOWN
          new Vector2Int(0,-1), //DOWN
          new Vector2Int(-1,-1), //DOWN-LEFT
          new Vector2Int(-1, 0), //LEFT
          new Vector2Int(-1, 1) //LEFT-UP
     };


     /// <summary>
     /// Metodo para obtener una dirección random
     /// </summary>
     /// <returns> Una posición 2D random aledaña </returns>
     public static Vector2Int GetRandomCardinalDirection()
     {
          return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
     }


     /// <summary>
     /// Metodo para obtener una dirección random
     /// </summary>
     /// <param name="seed"> Valor semilla para controlar los aleatorios </param>
     /// <returns> Una posición 2D random aledaña </returns>
     public static Vector2Int GetRandomCardinalDirection(Rnd rnd)
     {
          Debug.Log(rnd.Next(0, cardinalDirectionsList.Count));
          return cardinalDirectionsList[rnd.Next(0, cardinalDirectionsList.Count)];
     }

}
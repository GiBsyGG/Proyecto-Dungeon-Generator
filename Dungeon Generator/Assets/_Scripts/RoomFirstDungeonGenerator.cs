using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Rnd = System.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
     // Si se usa la semilla
     [SerializeField]
     private bool usePartitionSeed = false;
     [SerializeField]
     private int partitionSeed = 0;

     [SerializeField]
     private int minRoomWidth = 4, minRoomHeight = 4;

     // Area principal que dividiremos en rooms pequeños
     [SerializeField]
     private int dungeonWidth = 20, dungeonHeight = 20;

     // Para que las salas no queden unidas por el suelo si no que haya una separación asi sea de un wall
     [SerializeField]
     [Range(0, 10)]
     private int offset = 1;

     // Para revisar si usaremos el algoritmo de caminata aleatoria para crear las salas o usen todo el espacio del boundBox
     [SerializeField]
     private bool ramdomWalkRooms = false;


     protected override void RunProceduralGeneration()
     {
          CreateRooms();
     }

     /// <summary>
     /// Método que llama a los métodos para hallar el espacio de las salas y luego con esto, asignar el suelo y pintarlo con sus muros
     /// </summary>
     private void CreateRooms()
     {
          // Esto crea nuestra Bounding Boxes con el espacio para las rooms
          var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,
               new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

          if (usePartitionSeed)
          {
               // Esto crea nuestra Bounding Boxes con el espacio para las rooms
               roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,
                    new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight, partitionSeed);
          }
          HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

          // Si el reandomWalkRooms está activo creamos las rooms usando el random walk, si no, serán las rooms rectangulares
          if (ramdomWalkRooms)
          {
               floor = CreateRoomsRandomly(roomsList);
          }
          else
          {
               floor = CreateSimpleRooms(roomsList);
          }

          

          // Lista de los puntos centrales para conectar los corredores
          List<Vector2Int> roomCenters = new List<Vector2Int>();
          foreach (var room in roomsList)
          {
               // Añadimos el centro de cada room a la lista de centros, hay que castear a Vector2Int
               roomCenters.Add(((Vector2Int)Vector3Int.RoundToInt(room.center)));
          }

          HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
          // Unimos los corredores creados al suelo para pintarlos luego
          floor.UnionWith(corridors);

          // Luego de crear el suelo, lo pintamos y ponemos sus respectivos muros
          tilemapVisualizer.PaintFloorTiles(floor);
          WallGenerator.CreateWalls(floor, tilemapVisualizer);
     }


     /// <summary>
     /// Método que conectará todas las salas a partir de sus centros
     /// </summary>
     /// <param name="roomCenters"> Lista con los centros de las rooms a conectar </param>
     /// <returns> HashSet con los corredores creados para conectar las rooms </returns>
     private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
     {
          HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
          // Elejimos un punto random para iniciar los corredores
          var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
          if (usePartitionSeed)
          {
               // Elejimos un punto random para iniciar los corredores
               Rnd rnd = new Rnd(partitionSeed);
               currentRoomCenter = roomCenters[rnd.Next(0, roomCenters.Count)];
          }

          // Removemos al actual de la lista para que no se generen repeticiones en los corredores
          roomCenters.Remove(currentRoomCenter);

          while (roomCenters.Count > 0)
          {
               // Encontramos el punto mas cercano al centro actual para conectarlos
               Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
               roomCenters.Remove(closest);
               HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
               currentRoomCenter = closest;

               // Añadimos los nuevos corredores al HashSet a retornar
               corridors.UnionWith(newCorridor);
          }
          return corridors;

     }

     private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
     {
          HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();

          // Posicion inicial del corredor
          var position = currentRoomCenter;
          corridor.Add(position);

          // Viajaremos de arriba a abajo o visceversa hasta la posición destino del punto más cercano
          while (position.y != destination.y)
          {
               if (destination.y > position.y)
               {
                    position += Vector2Int.up;
               }
               else if (destination.y < position.y)
               {
                    position += Vector2Int.down;
               }
               corridor.Add(position);
          }
          // Viajaremos de izquierda a derecha o visceversa hasta la posición destino del punto más cercano
          while (position.x != destination.x)
          {
               if(destination.x > position.x)
               {
                    position += Vector2Int.right;
               }
               else if(destination.x < position.x)
               {
                    position += Vector2Int.left;
               }
               corridor.Add(position);
          }

          return corridor;
     }


     /// <summary>
     /// Método para encontrar el centro con la distancia mas cercana
     /// </summary>
     /// <param name="currentRoomCenter"> Posicion del centro actual </param>
     /// <param name="roomCenters"> Lista de centros para comparar </param>
     /// <returns> Posicion del centro mas cercano </returns>
     private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
     {
          Vector2Int closest = Vector2Int.zero;
          float distance = float.MaxValue;
          foreach (var position in roomCenters)
          {
               float currentDistance = Vector2.Distance(position, currentRoomCenter);
               if (currentDistance < distance)
               {
                    distance = currentDistance;
                    closest = position;
               }
          }
          return closest;
     }


     /// <summary>
     /// Método para crear rooms cuadradas en cada BoundBox
     /// </summary>
     /// <param name="roomsList"> Lista con los espacios en caja para cada room </param>
     /// <returns> El HashSet con las posiciones del suelo que ocuparán las rooms </returns>
     private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
     {
          HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
          foreach (var room in roomsList)
          {
               // Aquí usamos el offset para saber desde donde comenzamos a pintar el suelo y haya ese espacio entre rooms
               for (int col = offset; col < room.size.x - offset; col++)
               {
                    for (int row = offset; row < room.size.y - offset; row++)
                    {
                         // Encontramos la posición por cada Tile y la añadimos al suelo
                         Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row); 
                         floor.Add(position);
                    }
               }
          }
          return floor;
     }


     private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
     {
          HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
          for (int i = 0; i < roomsList.Count; i++)
          {
               var roomBounds = roomsList[i];
               // Esta es la forma de pasar parametros del bounds a posiciones 2D, en createRooms usamos otra forma que es con casteo
               var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
               // Llamamos al método para iniciar el randomWalk
               var roomfloor = RunRandomWalk(randomWalkParameters, roomCenter);

               // Hay que tener en cuenta el offset para el espaciado entre salas
               foreach (var position in roomfloor)
               {
                    if(position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) &&
                         position.y >= (roomBounds.yMin + offset) && position.y <= (roomBounds.yMax - offset))
                    {
                         floor.Add(position);
                    }
               }
          }
          return floor;
     }

}

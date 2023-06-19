using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Rnd = System.Random;
public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
     // Parametros para seed
     [SerializeField]
     private bool useCorridorsSeed = false;
     [SerializeField]
     private int corridorsSeed;

     // PCG parameters
     [SerializeField]
     private int corridorLenght = 14, corridorCount = 5;

     // Porcentaje de la sala que crearemos
     // El decorador Rango es para mantenerlo en ese rango ya que es porcentaje y evitar errores
     [SerializeField]
     [Range(0.1f,1)]
     private float roomPercent = 0.8f;

 


     // PCG Data
     private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

     private HashSet<Vector2Int> floorPositions, corridorPositions;

     // Gizmos Data
     private List<Color> roomColors = new List<Color>();


     protected override void RunProceduralGeneration()
     {
          CorridorFirstGeneration();
     }

     /// <summary>
     /// Llamar� a los m�todos para crear los corredores primero y luego pintar el suelo con sus murosS
     /// </summary>
     private void CorridorFirstGeneration()
     {
          HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

          // tendremos unas posiciones de potenciales salas
          HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

          // Llamamos el m�todo para crear los corredores, que ahora pintar� uno m�s amplio
          List<List<Vector2Int>> corridors =  CreateCorridors(floorPositions, potentialRoomPositions);

          // Crearemos las posiciones de las rooms
          HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

          // Buscaremos las Dead ends positions
          List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

          // Crearemos una room en cada dead end para consistencia en el Dungeon
          // roomPositions es necesario para saber si ya creamos una room en ese Dead end
          CreateRoomsAtDeadEnds(deadEnds, roomPositions);

          // Unimos las posiciones de las rooms con las del suelo, que ya contienen los corredores
          floorPositions.UnionWith(roomPositions);

          // Usaremos la posicion de los corredores para aumentar el tama�o de estos
          for (int i = 0; i < corridors.Count; i++)
          {
               // corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
               corridors[i] = IncreaseCorridorBrush3By3(corridors[i]);

               //A�adimos la ampliaci�n del corredor a las posiciones del floor
               floorPositions.UnionWith(corridors[i]);
               
          }

          // Pintamos el suelo ya con los corredores creados y luego los muros
          // Recordar que tilemapVisualizer tambi�n se hereda
          tilemapVisualizer.PaintFloorTiles(floorPositions);
          WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
     }


     /// <summary>
     /// Metodo para crear las salas en los Dead ends comprobando si no hay una room creada all� ya
     /// </summary>
     /// <param name="deadEnds"> Posiciones para crear las rooms </param>
     /// <param name="roomPositions"> Posiciones donde ya se est�n creando rooms </param>
     private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
     {
          foreach (var position in deadEnds)
          {
               // Creamos una room en caso de que esa posici�n no sea una room todav�a
               if(roomFloors.Contains(position) == false)
               {
                    var room= RunRandomWalk(randomWalkParameters, position);
                    // A�adimos la nueva room a los roomFloors
                    roomFloors.UnionWith(room);
               }
          }
     }


     /// <summary>
     /// Encuentra las posiciones de corredores que no terminan en nada (dead ends)
     /// </summary>
     /// <param name="floorPositions"> posiciones antes de a�adir las de las rooms </param>
     /// <returns></returns>
     /// <exception cref="NotImplementedException"></exception>
     private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
     {
          List<Vector2Int> deadEnds = new List<Vector2Int>();

          // Para todas las posiciones en el floor miraremos si son dead ends
          foreach (var position in floorPositions)
          {
               // Buscaremos los vecinos de la posicion que hacen parte de floorPostitions HashSet para saber cuantos tiene
               int neighboursCount = 0;
               foreach (var direction in Direction2D.cardinalDirectionsList)
               {
                    if(floorPositions.Contains(position + direction))
                    {
                         neighboursCount++;
                    }
                    
               }
               // Si tiene solo un vecino ese punto es un dead end
               if (neighboursCount == 1)
               {
                    deadEnds.Add(position);
               }
          }

          return deadEnds;
     }


     /// <summary>
     /// M�todo para crear las rooms
     /// </summary>
     /// <param name="potentialRoomPositions"> posiciones potenciales para las rooms </param>
     /// <returns> HashSet con las posiciones donde se crear�n las rooms </returns>
     /// <exception cref="NotImplementedException"></exception>
     private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
     {
          HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

          // Aqui usamos el parametro de roomPercent para ver cuantas de las posiciones potenciales disponibles tomamos para la rooms
          // Esta es la cantidad de rooms que generaremos
          int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

          // Para randomizar que puntos potenciales ser�n ocupados por salas
          // Guid.NewGuid(), nos crear� un identificador global �nico que b�sicamente es random el orden en el que elige ordenar
          // la librer�a Ling nos permite usar Take, para tomar hasta un rango n�merico de valores
          List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

          foreach (var roomPosition in roomsToCreate)
          {
               // Generaremos las posiciones del suelo del room con los parametros heredados para la room y la posici�n potencial para la room
               var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);

               // Guardamos los datos del room
               SaveRoomData(roomPosition, roomFloor);

               // A�adimos las posiciones a las posiciones de todas las rooms
               roomPositions.UnionWith(roomFloor);
          }

          return roomPositions;

     }

     
     /// <summary>
     /// Guarda los datos de una room en el diccionario de roomsDictionary
     /// </summary>
     /// <param name="roomPosition"> Posici�n central de la room </param>
     /// <param name="roomFloor"> Suelo que conforma la room </param>
     private void SaveRoomData(Vector2Int roomPosition, HashSet<Vector2Int> roomFloor)
     {
          roomsDictionary[roomPosition] = roomFloor;
          // Generamos un nuevo color de Room
          roomColors.Add(UnityEngine.Random.ColorHSV());
     }


     /// <summary>
     /// Crear� los corredores, la cantidad de corredores depende de corridorCount y estas se unir�n a las posiciones del floor
     /// </summary>
     /// <param name="floorPositions"></param>
     /// <returns> Lista de listas con las posiciones donde se pintar� el corredor </returns>
     private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
     {
          // Esta startPosition la heredamos del padre
          var currentPosition = startPosition;
          potentialRoomPositions.Add(currentPosition);
          List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
          Rnd rnd = new Rnd(corridorsSeed);

          for (int i = 0; i < corridorCount; i++)
          {
               if (useCorridorsSeed)
               {
                    var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLenght, rnd);
                    corridors.Add(corridor);

                    // Pasamos la �ltima posici�n como actual para generar el siguente corredor asegurandonos que los corredores est�n conectados
                    currentPosition = corridor[corridor.Count - 1];

                    // pasamos las currentPosition a las ubicaciones de salas potenciales
                    potentialRoomPositions.Add(currentPosition);

                    // Unimos esas posiciones del corredor con las posiciones que tenemos de suelo, as� el corredor tambi�n quedar� en el path
                    floorPositions.UnionWith(corridor);
               } else
               {
                    var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLenght);
                    corridors.Add(corridor);

                    // Pasamos la �ltima posici�n como actual para generar el siguente corredor asegurandonos que los corredores est�n conectados
                    currentPosition = corridor[corridor.Count - 1];

                    // pasamos las currentPosition a las ubicaciones de salas potenciales
                    potentialRoomPositions.Add(currentPosition);

                    // Unimos esas posiciones del corredor con las posiciones que tenemos de suelo, as� el corredor tambi�n quedar� en el path
                    floorPositions.UnionWith(corridor);
               }


          }
          corridorPositions = new HashSet<Vector2Int>(floorPositions);
          return corridors;
     }


     /// <summary>
     /// M�todo que incrementa en 1 el tama�o del corredor
     /// </summary>
     /// <param name="vector2Ints"> Corredor al que se incrementa el tama�o </param>
     /// <returns></returns>
     private List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
     {
          List<Vector2Int> newCorridor = new List<Vector2Int>();

          // Esto nos ayudar� a encontrar las esquinas
          Vector2Int previewsDirection = Vector2Int.zero;

          // Queremos verificar la direcci�n del primer Tile hacia el segundo, por eso empezamos desde el segundo
          for (int i = 1; i < corridor.Count; i++)
          {
               // Esto nos dar� la direcci�n
               Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
               if (previewsDirection != Vector2Int.zero &&
                    directionFromCell != previewsDirection)
               {
                    // Manejador de esquinas, si es esquina pintaremos un 3x3
                    for (int x = -1; x < 2; x++)
                    {
                         for (int y = -1; y < 2; y++)
                         {
                              newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                         }
                    }
                    // Con esto cambiamos de direcci�n y podremos encontrar otra esquina
                    previewsDirection = directionFromCell;
               }
               else
               {
                    // A�ade una celda en la direcci�n + 90 grados
                    Vector2Int newCorridorTileOffSet = GetDirection90From(directionFromCell);
                    newCorridor.Add(corridor[i - 1]);
                    // Esta es la Tile que se pinta a la derecha de la actual Tile
                    newCorridor.Add(corridor[i - 1] + newCorridorTileOffSet);
                    previewsDirection = directionFromCell;
               }
          }

          return newCorridor;
     }

     /// <summary>
     /// Rota la direcci�n pasada en 90 grados
     /// </summary>
     /// <param name="direction"> Direcci�n actual a rotar </param>
     /// <returns> Direcci�n rotada 90 grados</returns>

     private Vector2Int GetDirection90From(Vector2Int direction)
     {
          if (direction == Vector2Int.up)
               return Vector2Int.right;
          if (direction == Vector2Int.right)
               return Vector2Int.down;
          if (direction == Vector2Int.down)
               return Vector2Int.left;
          if (direction == Vector2Int.left)
               return Vector2Int.up;
          return Vector2Int.zero;
     }


     /// <summary>
     /// M�todo para ampliar el corredor cambiando cada posici�n a un espacio 3x3
     /// </summary>
     /// <param name="corridor"> corredor que ser� ampliado </param>
     /// <returns> Lista con las posiciones del nuevo corredor ampliado </returns>
     private List<Vector2Int> IncreaseCorridorBrush3By3(List<Vector2Int> corridor)
     {
          List<Vector2Int> newCorridor = new List<Vector2Int>();

          // A�adiremos un offset de celdas 3x3, no solo en las esquinaso
          for (int i = 1; i < corridor.Count; i++)
          {
               for (int x = -1; x < 2; x++)
               {
                    for (int y = -1; y < 2; y++)
                    {
                         newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
               }
          }
          return newCorridor;
     }

}

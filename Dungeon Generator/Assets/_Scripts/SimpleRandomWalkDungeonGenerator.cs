using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Rnd = System.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{

     // Usamos este ScriptableObject para definir los parametros que usará el algoritmo
     [SerializeField]
     protected SimpleRandomWalkSO randomWalkParameters;
     [SerializeField]
     protected Boolean useSeed = false;


     protected override void RunProceduralGeneration()
     {
          // Posiciones del suelo
          HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);

          // Antes de visualizar el nuevo suelo limpiaremos el anterior
          tilemapVisualizer.Clear();

          // Pintamos el floor del mapa
          tilemapVisualizer.PaintFloorTiles(floorPositions);

          // Pintamos los walls del mapa, este necesita los floors positions para pintarse
          WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
     }

     /// <summary>
     /// Correrá el algoritmo Random walk según un número de iteraciones dado
     /// </summary>
     /// <param name="parameters"> Contiene los parametros para el algoritmo </param>
     /// <param name="position"> Posición en la que iniciará el algoritmo </param>
     /// <returns> HashSet con las posiciones del suelo resultantes del randomWalk </returns>

     protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
     {
          var currentPosition = position;
          HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
          Rnd rnd = new Rnd(parameters.seed);

          //iteramos las veces que queremos correr el algoritmo
          for (int i = 0; i < parameters.iterations; i++)
          {
               if (useSeed)
               {
                    var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLenght, rnd);
                    // Union nos permite añadir las posiciones generadas en path a las de floorPositions, asegurando que no haya duplicados
                    floorPositions.UnionWith(path);
               }
               else
               {
                    var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLenght);
                    // Union nos permite añadir las posiciones generadas en path a las de floorPositions, asegurando que no haya duplicados
                    floorPositions.UnionWith(path);

               }
               // Para no comenzar de nuevo el algoritmo desde la misma posición, sino desde una posición random en el path o las floorPosition, asegurandonos que
               // la nueva iteración generada estará conectada al suelo previamente creado
               if(parameters.startRandomlyEachIteration)
               {
                    // Como HashSet no es indexado debemos importar esto (using System.Linq) para acceder a un elemento random
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
               }
          }
          return floorPositions;

     }

}

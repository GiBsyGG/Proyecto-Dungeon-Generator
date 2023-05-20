using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
     [SerializeField]
     protected TilemapVisualizer tilemapVisualizer = null;
     [SerializeField]
     protected Vector2Int startPosition = Vector2Int.zero;

     /// <summary>
     /// Metodo que será llamado puede ser desde el editor personalizado u otro Script, genera el Dungeon
     /// </summary>
     public void GenerateDungeon()
     {
          // Primero limpiaremos el Tilemap existente
          tilemapVisualizer.Clear();
          RunProceduralGeneration();
     }

     /// <summary>
     /// Generará el Tilemap acorde al algoritmo que elijamos
     /// </summary>
     protected abstract void RunProceduralGeneration();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
     [SerializeField]
     private Tilemap floorTilemap, wallTilemap;

     // Este es el tipo de Tile que queremos pintar de los que queremos en el folder precreados
     [SerializeField]
     private TileBase floorTile, wallTop;

     /// <summary>
     /// Método para pintar el suelo en el Tilemap
     /// </summary>
     /// <param name="floorPositions"></param>
     public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
     {
          PaintTiles(floorPositions, floorTilemap, floorTile);
     }


     /// <summary>
     /// Método para pintar el tilemap según las posiciones indicadas
     /// </summary>
     /// <param name="positions"> Posiciones que se usarán </param>
     /// <param name="Tilemap"> Tilemap en el que pintaremos </param>
     /// <param name="Tile"> Tipo de Tile a pintar </param>
     /// <exception cref="NotImplementedException"></exception>
     private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
     {
          foreach (var position in positions)
          {
               PaintSingleTile(tilemap, tile, position);
          }
     }


     /// <summary>
     /// Pintará un Tile único en una posición del Tilemap
     /// </summary>
     /// <param name="tilemap"> Tilemap en el que se pintará el Tile </param>
     /// <param name="tile"> Tipo de Tile que se pintará </param>
     /// <param name="position"> Posición en la cual se pintará el Tile </param>
     private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
     {
          var tilePosition = tilemap.WorldToCell((Vector3Int)position);
          tilemap.SetTile(tilePosition, tile);
     }


     /// <summary>
     /// Pintará un Wall único en una posición del Tilemap
     /// </summary>
     /// <param name="position"> Posición en la que se pintará el muro </param>
     internal void PaintSingleBasicWall(Vector2Int position)
     {
          // Llamaremos el método para pintar tiles
          PaintSingleTile(wallTilemap, wallTop, position);
     }

     public void Clear()
     {
          floorTilemap.ClearAllTiles();
          wallTilemap.ClearAllTiles();
     }

}

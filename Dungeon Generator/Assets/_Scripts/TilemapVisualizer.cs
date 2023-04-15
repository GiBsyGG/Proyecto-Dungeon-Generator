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
     /// M�todo para pintar el suelo en el Tilemap
     /// </summary>
     /// <param name="floorPositions"></param>
     public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
     {
          PaintTiles(floorPositions, floorTilemap, floorTile);
     }


     /// <summary>
     /// M�todo para pintar el tilemap seg�n las posiciones indicadas
     /// </summary>
     /// <param name="positions"> Posiciones que se usar�n </param>
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
     /// Pintar� un Tile �nico en una posici�n del Tilemap
     /// </summary>
     /// <param name="tilemap"> Tilemap en el que se pintar� el Tile </param>
     /// <param name="tile"> Tipo de Tile que se pintar� </param>
     /// <param name="position"> Posici�n en la cual se pintar� el Tile </param>
     private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
     {
          var tilePosition = tilemap.WorldToCell((Vector3Int)position);
          tilemap.SetTile(tilePosition, tile);
     }


     /// <summary>
     /// Pintar� un Wall �nico en una posici�n del Tilemap
     /// </summary>
     /// <param name="position"> Posici�n en la que se pintar� el muro </param>
     internal void PaintSingleBasicWall(Vector2Int position)
     {
          // Llamaremos el m�todo para pintar tiles
          PaintSingleTile(wallTilemap, wallTop, position);
     }

     public void Clear()
     {
          floorTilemap.ClearAllTiles();
          wallTilemap.ClearAllTiles();
     }

}

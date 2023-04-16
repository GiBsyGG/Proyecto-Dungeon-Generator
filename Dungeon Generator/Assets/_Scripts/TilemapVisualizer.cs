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
     private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
          wallInnerCornerDownLeft, wallInnerCornerDownRight,
          wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

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
     /// <param name="binaryType"> Tipo de muro que se va a pintar </param>
     internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
     {
          // Convertimos el valor que llega como String a binario (0b000100) por ejemplo
          int typeAsInt = Convert.ToInt32(binaryType, 2);
          TileBase tile = null;

          // Comprobamos el tipo de muro que es para usar su correspondiente Tile
          if (WallTypesHelper.wallTop.Contains(typeAsInt))
          {
               tile = wallTop;
          }
          else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
          {
               tile = wallSideRight;
          }
          else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
          {
               tile = wallBottom;
          }
          else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
          {
               tile = wallSideLeft;
          }
          else if (WallTypesHelper.wallFull.Contains(typeAsInt))
          {
               tile = wallFull;
          }


          if (tile != null)
          {
               // Llamaremos el método para pintar tiles si es nulo el tipo
               PaintSingleTile(wallTilemap, tile, position);
          }


          
     }


     /// <summary>
     /// Método para pintar las esquinas del tilemap
     /// </summary>
     /// <param name="position"> Posición de la esquina a pintar </param>
     /// <param name="BinaryType"> Tipo de muro que se va a pintar </param>
     internal void PaintSingleCornerWall(Vector2Int position, string BinaryType)
     {
          int typeAsInt = Convert.ToInt32(BinaryType, 2);
          TileBase tile = null;

          // Comprobamos el tipo de esquina que es
          if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
          {
               tile = wallInnerCornerDownLeft;
          }
          else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
          {
               tile = wallInnerCornerDownRight;
          }
          else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
          {
               tile = wallDiagonalCornerDownLeft;
          }
          else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
          {
               tile = wallDiagonalCornerDownRight;
          }
          else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
          {
               tile = wallDiagonalCornerUpLeft;
          }
          else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
          {
               tile = wallDiagonalCornerUpRight;
          }
          else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
          {
               tile = wallFull;
          }
          else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
          {
               tile = wallBottom;
          }

          if (tile != null)
          {
               // Si el tipo de tile existe lo pintamos
               PaintSingleTile(wallTilemap, tile, position);
          }
     }


     /// <summary>
     /// Método para limpiar todas las Tiles del mapa
     /// </summary>
     public void Clear()
     {
          floorTilemap.ClearAllTiles();
          wallTilemap.ClearAllTiles();
     }
}

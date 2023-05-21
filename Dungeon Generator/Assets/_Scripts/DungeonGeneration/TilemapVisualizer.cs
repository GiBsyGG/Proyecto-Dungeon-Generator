using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
     [SerializeField]
     private Tilemap floorTilemap, wallTilemap, cornerDecoTilemap;

     // Este es el tipo de Tile que queremos pintar de los que queremos en el folder precreados
     [SerializeField]
     private TileBase floorTile_0, floorTile_1, floorTile_2, floorTile_3,
          wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
          wallInnerCornerDownLeft, wallInnerCornerDownRight,
          wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

     [SerializeField]
     private TileBase tileLargeCandle, tileShortCandle, tileNoCandle, tileTorch, tileSpiderWeb, bones1, bones2;

     private List<TileBase> listaFloors;

     /// <summary>
     /// Método para pintar el suelo en el Tilemap
     /// </summary>
     /// <param name="floorPositions"></param>
     public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
     {
          listaFloors = new List<TileBase>();
          // Añadir los diferentes pisos para seleccionar uno al azar
          listaFloors.Add(floorTile_0);
          listaFloors.Add(floorTile_1);
          listaFloors.Add(floorTile_2);
          listaFloors.Add(floorTile_3);

          PaintTiles(floorPositions, floorTilemap, listaFloors);
     }


     /// <summary>
     /// Método para pintar el tilemap según las posiciones indicadas
     /// </summary>
     /// <param name="positions"> Posiciones que se usarán </param>
     /// <param name="Tilemap"> Tilemap en el que pintaremos </param>
     /// <param name="Tiles"> lista de Posibles tiles a pintar </param>
     /// <exception cref="NotImplementedException"></exception>
     private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, List<TileBase> tiles)
     {

          foreach (var position in positions)
          {
               TileBase tile = tiles[UnityEngine.Random.Range(0, 4)];
               PaintSingleTile(tilemap, tile, position);
          }
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
     /// Método para pintar las esquinas del tilemap
     /// </summary>
     /// <param name="position"> Posición de la esquina a pintar </param>
     /// <param name="BinaryType"> Tipo de muro que se va a pintar </param>
     internal void PaintSingleCornerDecoration(Vector2Int position, string BinaryType)
     {
          int typeAsInt = Convert.ToInt32(BinaryType, 2);
          TileBase tile = null;

          // Lista de decoraciones por esquina
          List<TileBase> cornerTilesLU = new List<TileBase>();
          List<TileBase> cornerTilesRU = new List<TileBase>();
          List<TileBase> cornerTilesRD = new List<TileBase>();
          List<TileBase> cornerTilesLD = new List<TileBase>();
          cornerTilesLU.Add(tileTorch);
          cornerTilesLU.Add(bones2);
          cornerTilesLU.Add(tileNoCandle);
          cornerTilesRU.Add(tileNoCandle);
          cornerTilesRU.Add(bones1);
          cornerTilesRU.Add(tileLargeCandle);
          cornerTilesRU.Add(tileShortCandle);
          cornerTilesLD.Add(tileSpiderWeb);
          cornerTilesRD.Add(bones1);
          cornerTilesRD.Add(bones2);



          // Comprobamos el tipo de esquina que es

          if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
          {
               tile = cornerTilesLU[UnityEngine.Random.Range(0,3)];
               Vector2Int offset = Direction2D.diagonalDirectionsList[1];
               position += offset;
          }
          else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
          {
               tile = cornerTilesRU[UnityEngine.Random.Range(0,4)];
               Vector2Int offset = Direction2D.diagonalDirectionsList[2];
               position += offset;
          }
          else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
          {
               tile = cornerTilesLD[0];
               Vector2Int offset = Direction2D.diagonalDirectionsList[0];
               position += offset;
          }
          else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
          {
               tile = cornerTilesRD[UnityEngine.Random.Range(0, 2)];
               Vector2Int offset = Direction2D.diagonalDirectionsList[3];
               position += offset;
          }

          // No siempre decoraremos la esquina
          if (UnityEngine.Random.Range(0,2) == 1)
          {
               tile = null;
          }

          if (tile != null)
          {
               // Si el tipo de tile existe lo pintamos
               PaintSingleTile(cornerDecoTilemap, tile, position);
          }

     }
          /// <summary>
          /// Método para limpiar todas las Tiles del mapa
          /// </summary>
          public void Clear()
     {
          floorTilemap.ClearAllTiles();
          wallTilemap.ClearAllTiles();
          cornerDecoTilemap.ClearAllTiles();
     }
}

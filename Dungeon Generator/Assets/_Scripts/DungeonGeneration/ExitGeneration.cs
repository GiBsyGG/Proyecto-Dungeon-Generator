using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExitGeneration
{
     public static void GenerateExit(Vector2Int initialPosition)
     {
          // Acceder al objeto player en la jerarquía
          GameObject ExitObject = GameObject.Find("Exit");
          if (ExitObject != null)
          {
               ExitObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, 0);
          }
     }
}

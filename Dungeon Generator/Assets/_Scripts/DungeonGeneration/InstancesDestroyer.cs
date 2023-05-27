using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InstancesDestroyer
{
     public static void DestroyInstances()
     {

          GameObject[] instances = GameObject.FindGameObjectsWithTag("EnemySquare");
          foreach (GameObject instance in instances)
          {
               GameObject.Destroy(instance);
          }

     }
}

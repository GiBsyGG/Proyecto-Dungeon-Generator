using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStarter : MonoBehaviour
{
     [SerializeField]
     private AbstractDungeonGenerator generator;

     // Start is called before the first frame update
     void Start()
    {
          if (generator != null)
          {
               //generator.GenerateDungeon();
          }
     }
}

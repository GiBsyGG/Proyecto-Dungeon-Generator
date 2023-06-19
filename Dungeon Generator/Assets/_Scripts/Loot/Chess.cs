using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour
{
     [SerializeField]
     Loot[] _loots;


     // Cuando el player interaccione debe abrirse
     public void Open()
     {
          int randomLoot = Random.Range(0, _loots.Length);
          // TODO: Instanciar el loot en un area cerca y con rotacion random
          Instantiate(_loots[randomLoot], transform.position + (Vector3)Random.insideUnitCircle, Random.rotation);
     }
}

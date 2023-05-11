using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
     private Rigidbody2D _rb;
 
    // Start is called before the first frame update
    void Start()
    {
          _rb = GetComponent<Rigidbody2D>();
    }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          Debug.Log("Melee with " + collision.name);
     }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
     private Rigidbody2D _rb;

     [SerializeField]
     private LayerMask _collisionMask;

     // Start is called before the first frame update
     void Start()
    {
          _rb = GetComponent<Rigidbody2D>();
    }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.transform.TryGetComponent(out IDamageable targetHit))
          {
               targetHit.TakeHit();
          }
     }
}

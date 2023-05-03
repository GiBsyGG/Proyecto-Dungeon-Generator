using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     public float speed = 10f;

     private void Update()
     {
          float horizontal = Input.GetAxisRaw("Horizontal");
          float vertical = Input.GetAxisRaw("Vertical");

          Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

          transform.position += direction * speed * Time.deltaTime;
     }
}

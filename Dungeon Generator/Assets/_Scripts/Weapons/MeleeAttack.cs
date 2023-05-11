using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
     [SerializeField]
     private float _speedRotation = 5;

     [SerializeField]
     private Transform _rotationPoint;

     [SerializeField]
     private GameObject _weapon;

     private bool isRotating = false;

     private void Update()
     {
          if (Input.GetButtonDown("Fire2") && !isRotating)
          {
               isRotating = true;

               // Activamos el weapon
               _weapon.SetActive(true);

               StartCoroutine(RotateObject());
          }
     }

     // Corrutina para la rotación del objeto
     IEnumerator RotateObject()
     {
          Quaternion startRotation = _rotationPoint.rotation * Quaternion.Euler(0f, 0f, 90f);
          Quaternion targetRotation = Quaternion.Euler(0f, 0f, 180f) * startRotation; // Rotar el objeto 180 grados en z

          float elapsedTime = 0f;
          float rotationTime = 0.3f; // Duracion de la rotacion

          // Animación de ataque a melee
          while (elapsedTime < rotationTime)
          {
               elapsedTime += Time.deltaTime;
               float t = elapsedTime / rotationTime;
               _weapon.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t * _speedRotation);
               yield return null;
          }

          // Terminamos la rotacion y apagamos el weapon
          _weapon.SetActive(false);
          isRotating = false;
          

     }
}

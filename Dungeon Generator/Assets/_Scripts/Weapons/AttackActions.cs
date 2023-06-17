using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActions : MonoBehaviour
{

     [SerializeField]
     private GameObject _projectilePrefab;

     [SerializeField]
     private Transform _shootPoint;

     [SerializeField]
     private float _meleeSpeed = 5;

     [SerializeField]
     private Transform _meleePoint;

     [SerializeField]
     private GameObject _meleeWapon;

     [SerializeField]
     private GameObject _distWapon;

     private bool _isMelee = false;

     private void Update()
     {
          // Shoot Gun
          if (Input.GetButtonDown("Fire1") && !_isMelee)
          {
               Shoot();
          }

          // Melee Attack
          if (Input.GetButtonDown("Fire2") && !_isMelee)
          {
               Melee();
          }
     }

     private void Shoot() {
          GameObject projectile = Instantiate(_projectilePrefab);
          projectile.transform.position = _shootPoint.position;
          projectile.transform.rotation = _shootPoint.rotation;
     }

     private void Melee() {
          _isMelee = true;

          // Activamos el melee y desactivamos el fusil
          _meleeWapon.SetActive(true);
          _distWapon.SetActive(false);

          StartCoroutine(RotateObject());
     }

     // Corrutina para la rotación del ataque a melee
     IEnumerator RotateObject()
     {
          Quaternion startRotation = _meleePoint.rotation * Quaternion.Euler(0f, 0f, 90f);
          Quaternion targetRotation = Quaternion.Euler(0f, 0f, 180f) * startRotation; // Rotar el objeto 180 grados en z

          float elapsedTime = 0f;
          float rotationTime = 0.3f; // Duracion de la rotacion

          // Animación de ataque a melee
          while (elapsedTime < rotationTime)
          {
               elapsedTime += Time.deltaTime;
               float t = elapsedTime / rotationTime;
               _meleeWapon.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t * _meleeSpeed);
               
               yield return null;
          }

          // Terminamos la rotacion, apagamos el melee y encendemos el arma de nuevo
          _meleeWapon.SetActive(false);
          _distWapon.SetActive(true);
          _isMelee = false;
     }

}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [SerializeField]
     private float speed = 10f;

     private Rigidbody2D _rb;
     private Camera _cam;

     // Cuerpo del player
     [Space(20)]
     [SerializeField]
     private Transform _body;

     // Componente animator del Body
     private Animator _bodyAnimator;

     // Arma del player
     [Space(20)]
     [SerializeField]
     private Transform _weapon;
     private GunController _gunController;
     private Gun _gun;

     // Arma melee del player
     [Space(20)]
     [SerializeField]
     private Transform _meleePoint;

     // Flag para el sonido
     private bool _stepsSound = false;

     private void Start()
     {

          _gunController = GetComponent<GunController>();
          _rb = GetComponent<Rigidbody2D>();
          _cam = Camera.main;
          _bodyAnimator = _body.GetComponent<Animator>();
     }
     private void Update()
     {
          if(GameManager.Instance.gameState == GameState.InGame)
          {
               MovePlayer();
               RotateWeapon();
               RotateMeleePoint();
          }
     }

     private void MovePlayer()
     {
          // Movimiento del Body
          float horizontal = Input.GetAxisRaw("Horizontal");
          float vertical = Input.GetAxisRaw("Vertical");

          Vector2 _dir = new Vector2(horizontal, vertical);
          _dir.Normalize();
          _rb.velocity = _dir * speed;

          // Cambiar el estado de movimiento para el animator si se mueve
          if (_dir.x != 0 || _dir.y != 0) {
               _bodyAnimator.SetBool("isMoving", true);

               // Sonido de pasos
               if (!_stepsSound)
               {
                    StartCoroutine(PlayStepsSound());
               }
               
          } else
          {
               _bodyAnimator.SetBool("isMoving", false);
          }
     }

     private void RotateWeapon()
     {
          // Posicion del mouse 
          Vector2 mousePos = Input.mousePosition;
          Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
          mouseWorldPos.z = 0;

          // Rotacion del arma
          Vector3 aimVector = (mouseWorldPos - transform.position).normalized;
          float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;

          _weapon.rotation = Quaternion.Euler(0, 0, angle);
          _gun = _gunController.equippedGun;
          // Si la rotac�on va hacia atras del personaje invierte el Sprite del arma y el personaje
          if (_weapon.rotation.z < -0.5 || _weapon.rotation.z > 0.5)
          {
              if(_gun != null)
               {
                    SpriteRenderer sprite = _gun.spriteRenderer;
                    sprite.flipY = true;
               }
              
              transform.rotation = Quaternion.Euler(0, 180, 0);
          }
          else
          {
              if (_gun != null)
               {
                    SpriteRenderer sprite = _gun.spriteRenderer;
                    sprite.flipY = false;
               }  
              transform.rotation = Quaternion.Euler(0, 0, 0);
          }
     }

     private void RotateMeleePoint()
     {
          // Posicion del mouse 
          Vector2 mousePos = Input.mousePosition;
          Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
          mouseWorldPos.z = 0;

          // Rotacion del arma
          Vector3 aimVector = (mouseWorldPos - transform.position).normalized;
          float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;

          _meleePoint.rotation = Quaternion.Euler(0, 0, angle);

          // Acercamiento del arma a la direccion del mouse
          _meleePoint.position = transform.position - (new Vector3(0f, 0.2f, 0f)) + aimVector * 0.2f;
     }

     // Corrutina para evitar que se rompa el sonido al sonar de seguido
     IEnumerator PlayStepsSound()
     {
          AudioManager.Instance.PlaySound2D("Step");
          _stepsSound = true;

          while (true)
          {
               yield return new WaitForSeconds(0.20f);
               break;
          }
          _stepsSound = false;
     }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
     // Componente animator del Body
     private Animator _bodyAnimator;
     private Rigidbody2D _rigidbody;

     // Cuerpo del player
     [Space(20)]
     [SerializeField]
     private Transform _body;

     // Arma del player
     [Space(20)]
     [SerializeField]
     private GameObject _weapon;

     void Start()
     {
          InitHealth();
          _bodyAnimator = _body.GetComponent<Animator>();
          _rigidbody = GetComponent<Rigidbody2D>();
     }

     protected override void OnTakeDamage()
     {
          base.OnTakeDamage();
          //TODO: Communicate new health
     }

     protected override void OnDeath()
     {
          base.OnDeath();

          // Apagamos los scripts de movimiento y ataque
          this.TryGetComponent<AttackActions>(out AttackActions attackActions);
          if (attackActions != null)
               attackActions.enabled = false;
          this.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement);
          if (playerMovement!= null)
               playerMovement.enabled = false;

          // Detenemos el player
          _rigidbody.bodyType = RigidbodyType2D.Static;
          _weapon.SetActive(false);
          
          _bodyAnimator.SetBool("isDeath", true);
          
          GameManager.Instance.GameOver();
     }

     public void OnRevive()
     {
          InitHealth();
          
          // Apagamos la animacion de muerte
          _bodyAnimator.SetBool("isDeath", false);

          // Activamos el arma
          _weapon.SetActive(true);

          // Devolvemos el RB
          _rigidbody.bodyType = RigidbodyType2D.Dynamic;

          // Encendemos los scripts de movimiento y ataque
          this.TryGetComponent<AttackActions>(out AttackActions attackActions);
          attackActions.enabled = true;
          this.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement);
          playerMovement.enabled = true;
     }
}
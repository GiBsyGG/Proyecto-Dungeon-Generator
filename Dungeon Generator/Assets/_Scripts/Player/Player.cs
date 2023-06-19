using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
     private GunController _gunController;

     void Start()
     {
          InitHealth();
          _bodyAnimator = _body.GetComponent<Animator>();
          _rigidbody = GetComponent<Rigidbody2D>();
          _gunController = GetComponent<GunController>();
     }

     protected override void OnTakeDamage()
     {
          base.OnTakeDamage();
          
          GameEvents.OnPlayerHealthChangeEvent?.Invoke(HealthPoints);
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
          if (_rigidbody != null)
               _rigidbody.bodyType = RigidbodyType2D.Static;

          // Apagamos el arma
          if (_gunController != null)
               _gunController.equippedGun.gameObject.SetActive(false);

          if (_bodyAnimator != null)
               _bodyAnimator.SetBool("isDeath", true);

          GameManager.Instance.GameOver();
     }

     public void OnRevive()
     {
          InitHealth();
          
          // Apagamos la animacion de muerte
          if(_bodyAnimator != null)
               _bodyAnimator.SetBool("isDeath", false);

          // Indicamos que tiene las vidas de vuelta
          GameEvents.OnPlayerHealthChangeEvent?.Invoke(HealthPoints);

          // Activamos el arma
          if(_gunController != null)
               _gunController.equippedGun.gameObject.SetActive(true);

          // Devolvemos el RB
          if (_rigidbody != null)
               _rigidbody.bodyType = RigidbodyType2D.Dynamic;

          // Encendemos los scripts de movimiento y ataque
          this.TryGetComponent<AttackActions>(out AttackActions attackActions);
          attackActions.enabled = true;
          this.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement);
          playerMovement.enabled = true;
     }
}
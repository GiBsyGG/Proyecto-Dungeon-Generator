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
     public GunController _gunController { get; private set; }

     public bool HaveKey { get; set; }

     private bool _almostDeadSound;

     void Start()
     {
          InitHealth();
          _bodyAnimator = _body.GetComponent<Animator>();
          _rigidbody = GetComponent<Rigidbody2D>();
          _gunController = GetComponent<GunController>();

          GameEvents.OnBackToMenuEvent += OnInitialState;
     }

     private void OnDestroy()
     {
          GameEvents.OnBackToMenuEvent -= OnInitialState;
     }

     private void FixedUpdate()
     {
          if (GameManager.Instance.gameState == GameState.InGame)
          {
               if (HealthPoints <= 2 && !_almostDeadSound)
                    StartCoroutine(PlayAlmostDead());
          }
     }

     protected override void OnTakeDamage()
     {
          if(GameManager.Instance.gameState == GameState.InGame)
          {
               base.OnTakeDamage();

               GameEvents.OnPlayerHealthChangeEvent?.Invoke(HealthPoints);

               AudioManager.Instance.PlaySound2D("PlayerDamaged");
          }
          
     }

     protected override void OnDeath()
     {
          if (GameManager.Instance.gameState == GameState.InGame)
          {
               base.OnDeath();

               // Apagamos los scripts de movimiento y ataque
               this.TryGetComponent<AttackActions>(out AttackActions attackActions);
               if (attackActions != null)
                    attackActions.enabled = false;
               this.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement);
               if (playerMovement != null)
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
     }

     public void OnRevive()
     {
          InitHealth();
          
          // Apagamos la animacion de muerte
          if(_bodyAnimator != null)
               _bodyAnimator.SetBool("isDeath", false);

          // Le quitamos la llave
          HaveKey = false;

          // Indicamos que tiene las vidas de vuelta y que pierde la llave
          GameEvents.OnPlayerHealthChangeEvent?.Invoke(HealthPoints);
          GameEvents.OnPlayerKeyChangeEvent?.Invoke(HaveKey);

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

     public void OnInitialState()
     {
          // Reseteamos la vida y el arma
          InitHealth();
          _gunController.EquipInitialGun();
     }

     public void OnNewDungeon()
     {
          HaveKey = false;
          GameEvents.OnPlayerKeyChangeEvent?.Invoke(HaveKey);
     }

     // Corrutina para evitar que se rompa el sonido al sonar de seguido
     IEnumerator PlayAlmostDead()
     {
          AudioManager.Instance.PlaySound2D("PlayerAlmostDead");
          _almostDeadSound = true;

          while (true)
          {
               yield return new WaitForSeconds(3f);
               break;
          }

          _almostDeadSound = false;
     }
}
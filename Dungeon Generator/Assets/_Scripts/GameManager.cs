using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { InGame, InMenu, InDead, InLoad};

public class GameManager : MonoBehaviour
{
     public static GameManager Instance;
     public int dungeonLevel { get; private set; }

     public GameState gameState { get; private set; }

     [SerializeField]
     private GameObject menuScreen;

     [SerializeField]
     private GameObject nextDungeonScreen;

     [SerializeField]
     private GameObject deadScreen;

     [SerializeField]
     private AbstractDungeonGenerator generator;

     [SerializeField]
     public Player player;
     [SerializeField]
     public Exit exit;


     private void Awake()
     {
          if (Instance == null)
          {
               Instance = this;
               DontDestroyOnLoad(gameObject);
          }
          else
          {
               Destroy(gameObject);
          }
     }

     private void Start()
     {
          HandleMenu();
     }

     void Update()
     {
          if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.InGame)
          {
               HandleMenu();
          }

     }

     public void GameStart()
     {
          HandleGameplay();
     }

     public void DungeonStart()
     {
          HandleNextDungeon();
     }

     public void BackToMenu()
     {
          HandleMenu();
     }

     public void GameOver()
     {    
          gameState = GameState.InDead;

          // Activar el cursor
          Cursor.visible = true;
          deadScreen.SetActive(true);
          GameEvents.OnPlayerDeathEvent?.Invoke(Instance.dungeonLevel);

          AudioManager.Instance.PlaySound2D("PlayerDead");
     }

     void HandleMenu()
     {

          // Si se regresa al menú se resetea el Dungeon -> Temporal
          // TODO: Implementar persistencia para nueva partida o continuar uno
          dungeonLevel = 1;

          // Comunicar que se recomienzan las mazmorras
          GameEvents.OnChangeDungeonEvent?.Invoke(Instance.dungeonLevel);

          //SceneManager.LoadScene("StartMenu");
          menuScreen.SetActive(true);
          deadScreen.SetActive(false);
          nextDungeonScreen.SetActive(false);
          gameState = GameState.InMenu;

          // Activar el cursor
          Cursor.visible = true;

          GameEvents.OnBackToMenuEvent?.Invoke();

     }

     void HandleGameplay()
     {
          //StartCoroutine(LoadGameplayAsyncScene("Gameplay"));
          //SceneManager.LoadScene("Gameplay");

          // Dar un pequeño tiempo para resetear animaciones y activacion de GameObjects
          StartCoroutine(LoadGameplay());

          gameState = GameState.InGame;

          // Desactivar el cursor
          Cursor.visible = false;
     }

     void HandleNextDungeon()
     {
          Instance.dungeonLevel += 1;

          //SceneManager.LoadScene("NextDungeon");
          nextDungeonScreen.SetActive(true);

          gameState = GameState.InLoad;

          // Comunicar que se comenzó una mazmorra
          GameEvents.OnChangeDungeonEvent?.Invoke(Instance.dungeonLevel);

          AudioManager.Instance.PlaySound2D("LoadScreen");

          StartCoroutine(LoadNewDungeon());
     }

     IEnumerator LoadGameplayAsyncScene(string scene)
     {
          AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

          // Wait until the asynchronous scene fully loads
          while (!asyncLoad.isDone)
          {
               yield return null;
          }

          yield return new WaitForSeconds(3f);

          //TODO: Start Game
          // El Invoke previene posibles errores al llamarla
          //if (GameEvents.OnStartGameEvent!= null)
          //     GameEvents.OnStartGameEvent.Invoke();

          // Otra forma de lo de arriba, esto lo que hace es indicar que este evento sucedi�
          // y que los que necesitan saberlo realicen las acciones correspondientes 
          GameEvents.OnStartGameEvent?.Invoke();
     }

     IEnumerator LoadNewDungeon()
     {

          // Wait until the asynchronous scene fully loads
          while (true)
          {
               yield return new WaitForSeconds(2f);
               break;
          }

          // Se inicia un nuevo estado de juego pero sin revivir a l player
          gameState = GameState.InGame;

          // Desactivar el cursor
          Cursor.visible = false;

          generator.GenerateDungeon();
          menuScreen.SetActive(false);
          nextDungeonScreen.SetActive(false);
          deadScreen.SetActive(false);

     }

     IEnumerator LoadGameplay()
     {
          generator.GenerateDungeon();
          player.OnRevive();

          while (true)
          {
               yield return new WaitForSeconds(1f);
               break;
          }

          menuScreen.SetActive(false);
          nextDungeonScreen.SetActive(false);
          deadScreen.SetActive(false);
     }
}

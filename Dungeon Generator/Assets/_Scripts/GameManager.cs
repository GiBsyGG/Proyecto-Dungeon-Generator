using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { InGame, InMenu, InDead};

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

          dungeonLevel = 0;
          menuScreen.SetActive(true);
          gameState = GameState.InMenu;
     }

     void Update()
     {
          if (Input.GetKeyDown(KeyCode.Space))
          {
               HandleGameplay();
          }
          else if (Input.GetKeyDown(KeyCode.Escape))
          {
               HandleMenu();
          }

     }

     public void GameOver()
     {
          Debug.Log("Game over");
          Instance.dungeonLevel = 0;
          gameState = GameState.InDead;

          // Activar el cursor
          Cursor.visible = true;
          deadScreen.SetActive(true);
     }

     public void HandleMenu()
     {
          Debug.Log("Loading Menu...");
          //SceneManager.LoadScene("StartMenu");
          menuScreen.SetActive(true);
          deadScreen.SetActive(false);
          gameState = GameState.InMenu;

          // Activar el cursor
          Cursor.visible = true;
     }

     public void HandleGameplay()
     {
          Debug.Log("Loading Gameplay...");
          //StartCoroutine(LoadGameplayAsyncScene("Gameplay"));
          //SceneManager.LoadScene("Gameplay");
          menuScreen.SetActive(false);
          nextDungeonScreen.SetActive(false);
          deadScreen.SetActive(false);
          generator.GenerateDungeon();
          gameState = GameState.InGame;

          // Desactivar el cursor
          Cursor.visible = false;
     }

     public void HandleNextDungeon()
     {
          Debug.Log("Loading Next Dungeon...");
          Instance.dungeonLevel += 1;
          //SceneManager.LoadScene("NextDungeon");
          nextDungeonScreen.SetActive(true);
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
               yield return new WaitForSeconds(5f);
               break;
          }

          HandleGameplay();
     }
}

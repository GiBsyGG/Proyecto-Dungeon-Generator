using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
     }

     void HandleMenu()
     {
          Debug.Log("Loading Menu...");
          SceneManager.LoadScene("StartMenu");
     }

     void HandleGameplay()
     {
          Debug.Log("Loading Gameplay...");
          StartCoroutine(LoadGameplayAsyncScene("Gameplay"));
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
}

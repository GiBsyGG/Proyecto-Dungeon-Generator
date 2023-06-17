using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] GameObject _player;
     // Start is called before the first frame update
    void Start()
    {
          if (!_player)
          {
               _player = GameObject.FindWithTag("Player");
          }
     }

    // Update is called once per frame
    void Update()
    {
          // Mover la camara con la posición del player pero encima de el
        transform.position = _player.transform.position + new Vector3(0, 0, -10);
    }
}

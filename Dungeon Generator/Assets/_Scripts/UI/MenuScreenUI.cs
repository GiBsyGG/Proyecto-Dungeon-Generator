using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenUI : MonoBehaviour
{
    public void OnStartButtonClicked()
     {
          GameManager.Instance.GameStart();
     }
}

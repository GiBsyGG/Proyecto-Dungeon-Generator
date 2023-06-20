using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScreenUI : MonoBehaviour
{
     public void OnBackToMenuButtonClicked()
     {
          GameManager.Instance.BackToMenu();
     }
}

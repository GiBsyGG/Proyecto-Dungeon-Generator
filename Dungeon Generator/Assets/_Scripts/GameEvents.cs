using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
     public static Action OnStartGameEvent;

     // Game Over -> Points, new max Score, time, level
     // Points Update

     public static Action<int> OnPlayerHealthChangeEvent; // currentHealth

     public static Action<Enemy> OnEnemyDeath;  // Enemy
}

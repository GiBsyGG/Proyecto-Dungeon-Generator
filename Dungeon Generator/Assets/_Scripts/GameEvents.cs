using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
     public static Action OnStartGameEvent;

     // Game Over -> Points, new max Score, time, level
     // Points Update
     // Evento de posible interaccion? aun no necesario para el cambio de icono, tal vez para otra cosa

     public static Action<Sprite> OnGunChangeEvent;  // New gun Sprite

     public static Action<int> OnPlayerHealthChangeEvent; // currentHealth

     public static Action<int> OnPlayerDeathEvent; // DungeonLevel

     public static Action<Enemy> OnEnemyDeath;  // Enemy

     public static Action<int> OnChangeDungeonEvent; // DungeonLevel
}

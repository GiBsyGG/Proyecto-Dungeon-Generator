using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RandomDungeonGeneratorEditor : Editor
{
     AbstractDungeonGenerator generator;

     private void Awake()
     {
          // Obtenemos la referencia del generador, el target es lo que tendr� el inspector customizado, y hay que hacer el casteo
          generator = (AbstractDungeonGenerator)target;
     }

     // Crearemos un bot�n
     public override void OnInspectorGUI()
     {
          // Llamamos al m�todo base para mostrar nuestros campos de nuestra clase
          base.OnInspectorGUI();

          // Crearemos el bot�n, la sentencia if solo funciona si el bot�n es Clickeado
          if(GUILayout.Button("Create Dungeon"))
          {
               // Si el bot�n es presionado creamos el Dungeon
               generator.GenerateDungeon();
          }
     }
}

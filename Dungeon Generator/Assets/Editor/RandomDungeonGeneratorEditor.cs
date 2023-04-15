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
          // Obtenemos la referencia del generador, el target es lo que tendrá el inspector customizado, y hay que hacer el casteo
          generator = (AbstractDungeonGenerator)target;
     }

     // Crearemos un botón
     public override void OnInspectorGUI()
     {
          // Llamamos al método base para mostrar nuestros campos de nuestra clase
          base.OnInspectorGUI();

          // Crearemos el botón, la sentencia if solo funciona si el botón es Clickeado
          if(GUILayout.Button("Create Dungeon"))
          {
               // Si el botón es presionado creamos el Dungeon
               generator.GenerateDungeon();
          }
     }
}

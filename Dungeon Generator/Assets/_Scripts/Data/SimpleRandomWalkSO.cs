using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// El _ en el filename es para darle nombre a los diferentes tipos que habr�n
[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_",menuName ="PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject
{
     // Parametros que usaremos en el RandomWalk

     // Numero de veces que queremos correr el algoritmo RandomWalk y pasos que dar� el agente
     public int iterations = 10, walkLenght = 10;
     public bool startRandomlyEachIteration = true;

     // Manejo del valor semilla
     public int seed = 0;
}

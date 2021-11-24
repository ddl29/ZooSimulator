using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matriz : MonoBehaviour
{
    private System.Random rand = new System.Random();
    public float[,] matriz =  {{0f, 0.35f, 0.23f, 0.20f, 0.20f, 0.02f},
                        {0.37f, 0f, 0.21f, 0.22f, 0.19f, 0.01f},
                        {0.37f, 0.40f, 0f, 0.20f, 0.02f, 0.01f},
                        {0.20f, 0.25f, 0.02f, 0f, 0.43f, 0.10f},
                        {0.17f, 0.26f, 0.03f, 0.14f, 0f, 0.40f},
                        {0.06f, 0.11f, 0.01f, 0.34f, 0.48f, 0f}};

    public int getNextPlace(int actual, bool[] visitados) //index entre 0 y 5
    {   // TRUE        FALSE       FALSE       TRUE     FALSE     FALSE
        //Acuario 0, Pinguinos 1, Canguros 2, SkyZoo 3, Safari 4, Selva 5
        //{0f, 0.35f, 0.23f, 0.20f, 0.20f, 0.02f}
        float normalized = rand.Next(100) / 100.0f;
        int index = getIndex(normalized, actual);
        while (visitados[index])
        {
            normalized = rand.Next(100) / 100.0f;
            index = getIndex(normalized, actual);
        }
        return index;
    }

    private int getIndex(float random, int actual)
    {
        float acumulado = 0;
        for (int i = 0; i < 6; i++)
        {
            acumulado += matriz[actual, i];
            if (random <= acumulado)
            {
                return i;
            }
        }
        print("Random = " + random + " y Acumulado = " + acumulado);
        return -1;
    }
}

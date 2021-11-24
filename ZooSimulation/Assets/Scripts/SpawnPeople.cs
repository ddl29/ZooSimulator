using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPeople : MonoBehaviour
{
    public GameObject persona;
    public float tasaPorMinuto; //3 personas por minuto
    public Transform entrada;
    private int contLocal;
    private int minutosTranscurridos;
    public int horasLlegada; //1 hr = 60 segundos
    public Text minutos;

    void Start()
    {
        InvokeRepeating("PersonSpawn", 1, 1 / tasaPorMinuto);
    }

    void PersonSpawn()
    {
        Vector2 entradaVec = new Vector2(entrada.position.x, entrada.position.y);
        Instantiate(persona, entradaVec, Quaternion.identity);
        contLocal++;
        if (contLocal == tasaPorMinuto)
        {
            minutosTranscurridos++;
            minutos.text = "" + minutosTranscurridos;
            contLocal = 0;
        }
        if (minutosTranscurridos == horasLlegada * 60)
        {
            print(minutosTranscurridos);
            CancelInvoke("PersonSpawn");
        }
    }
}

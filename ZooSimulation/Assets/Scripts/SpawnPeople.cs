using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPeople : MonoBehaviour
{
    private Lugar[] lugares = new Lugar[6];
    public GameObject persona;
    public float tasaPorMinuto; //3 personas por minuto
    public Transform entrada;
    private int contLocal;
    private float time = 0.0f;
    private int minutosTranscurridos;
    private int offset = 30;
    public Text minutosText;
    private int hrs = 9;
    public Text hrsText;
    public int horasLlegada; //1 hr = 60 segundos
    private int aforoGlobal;
    public Text aforoGlobalText;
    public Text horasLlegadaText;
    private float pureTimer = 0;
    public Text abiertoText;
    private bool abierto;


    void Start()
    {
        lugares[0] = (Lugar)GameObject.Find("Acuario").GetComponent(typeof(Lugar));
        lugares[1] = (Lugar)GameObject.Find("Pinguinos").GetComponent(typeof(Lugar));
        lugares[2] = (Lugar)GameObject.Find("Canguros").GetComponent(typeof(Lugar));
        lugares[3] = (Lugar)GameObject.Find("SkyZoo").GetComponent(typeof(Lugar));
        lugares[4] = (Lugar)GameObject.Find("Safari").GetComponent(typeof(Lugar));
        lugares[5] = (Lugar)GameObject.Find("Selva").GetComponent(typeof(Lugar));
        abierto = true;
        int t = 9 + horasLlegada;
        horasLlegadaText.text = "" + t;
        InvokeRepeating("PersonSpawn", 1, 1 / tasaPorMinuto);
    }

    void Update()
    {
        pureTimer += Time.deltaTime;
        if (minutosTranscurridos == 60)
        {
            offset = 0;
            hrs++;
            //hrsText.text = "" + hrs;
            minutosTranscurridos = 0;
            time = 0;
            //minutosText.text = "" + minutosTranscurridos;
        }
        time += Time.deltaTime;
        minutosTranscurridos = offset + (int)Mathf.Floor(time);
        if (minutosTranscurridos < 10)
        {
            minutosText.text = "0" + minutosTranscurridos;
        }
        else
        {
            minutosText.text = "" + minutosTranscurridos;
        }
        if (hrs < 10)
        {
            hrsText.text = "0" + hrs + ":";
        }
        else
        {
            hrsText.text = "" + hrs + ":";
        }
        //CUANDO SE CIERRA EL ZOO
        if (Mathf.Floor(pureTimer) == horasLlegada * 60)
        {
            abierto = false;
            abiertoText.text = "- Cerrado -";
            CancelInvoke("PersonSpawn");
        }
        //CUANDO YA NO HAY NADIE
        if (lugares[0].isVacio() && lugares[1].isVacio() && lugares[2].isVacio() && lugares[3].isVacio() && lugares[4].isVacio() && lugares[5].isVacio())
        {
            if (aforoGlobal > 0)
            {
                GameObject[] restantes = GameObject.FindGameObjectsWithTag("Persona");
                foreach (GameObject restante in restantes)
                {
                    Destroy(restante, 2);
                    aforoGlobal--;
                    aforoGlobalText.text = "" + aforoGlobal;
                }
            }
        }
    }

    void PersonSpawn()
    {
        Vector2 entradaVec = new Vector2(entrada.position.x, entrada.position.y);
        Instantiate(persona, entradaVec, Quaternion.identity);
        aforoGlobal++;
        aforoGlobalText.text = "" + aforoGlobal;
    }

    public void decrementaAforGlobal()
    {
        this.aforoGlobal--;
        aforoGlobalText.text = "" + aforoGlobal;
    }

    public bool isAbierto()
    {
        return this.abierto;
    }
}

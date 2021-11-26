using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Persona : MonoBehaviour
{
    private Transform[] lugares = new Transform[6];
    private Transform entrada;
    private float speed = 5.0f;
    private Transform meta;
    private System.Random rand = new System.Random();
    private bool[] visitados = new bool[6];
    private int visitadosInt;
    IEnumerator rutinaWait;
    private Matriz matriz;
    private int actual;
    private bool turno;

    // Start is called before the first frame update
    void Start()
    {
        entrada = GameObject.Find("Entrada").transform;
        turno = false;
        gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        visitadosInt = 0;
        lugares[0] = GameObject.Find("Acuario").transform;
        lugares[1] = GameObject.Find("Pinguinos").transform;
        lugares[2] = GameObject.Find("Canguros").transform;
        lugares[3] = GameObject.Find("SkyZoo").transform;
        lugares[4] = GameObject.Find("Safari").transform;
        lugares[5] = GameObject.Find("Selva").transform;
        matriz = (Matriz)GameObject.Find("Matriz").GetComponent(typeof(Matriz));
        //Acuario 0, Pinguinos 1, Canguros 2, SkyZoo 3, Safari 4, Selva 5
        float[] probInicial = { 0.15f, 0.20f, 0.03f, 0.30f, 0.30f, 0.02f };
        double normalized = rand.Next(101) / 100.0;
        float acumulado = 0;
        for (int i = 0; i < probInicial.Length; i++)
        {
            acumulado += probInicial[i];
            if (normalized < acumulado)
            {
                meta = lugares[i];
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, meta.position, step);
        /*if (visitadosInt == 6)
        {
            print("Ya terminé");
            if (transform.position == entrada.position)
            {
                Destroy(gameObject);
            }
        }*/
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Lugar")
        {
            Lugar llegue = (Lugar)c.GetComponent(typeof(Lugar));
            actual = llegue.getIndice();
            visitados[actual] = true;
            visitadosInt++;
            float duracion = (float)llegue.getDuracion();
            rutinaWait = wait(duracion);
            //meta = lugares[matriz.getNextPlace(actual, visitados)];
        }
        if (visitadosInt == 6 && c.tag == "Entrada")
        {
            SpawnPeople exit = (SpawnPeople)c.GetComponent(typeof(SpawnPeople));
            exit.decrementaAforGlobal();
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (turno)
        {
            StartCoroutine(rutinaWait);
            turno = false;
        }
    }

    IEnumerator wait(float wait)
    {
        yield return new WaitForSeconds(wait);
        if (visitadosInt < 6)
        {
            meta = lugares[matriz.getNextPlace(actual, visitados)];

        }
        else
        {
            meta = entrada;
        }
    }

    public void setTurno(bool check)
    {
        this.turno = check;
    }

    public int getVisitadosInt()
    {
        return this.visitadosInt;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lugar : MonoBehaviour
{
    public int indice;
    public int cupo;
    public int duracion;
    private int cont = 0;
    public Text aforo;
    public Text filaTexto;
    private Queue<Persona> fila;
    private int contFila = 0;
    IEnumerator checarCupo;

    // Start is called before the first frame update
    void Start()
    {
        fila = new Queue<Persona>();
        aforo.text = "" + cont;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Persona")
        {
            Persona tmp = (Persona)c.GetComponent(typeof(Persona));
            if (cont < cupo)
            {
                tmp.setTurno(true);
                this.cont++;
                aforo.text = "" + cont;
            }
            else
            {
                tmp.setTurno(false);
                fila.Enqueue(tmp);
                contFila++;
                filaTexto.text = "" + contFila;
                checarCupo = checarCupoCorutina();
                StartCoroutine(checarCupo);
            }
        }
    }

    IEnumerator checarCupoCorutina()
    {
        while (true)
        {
            if (contFila > 0 && cont < cupo) //se acaba de salir alguien del aforo
            {
                fila.Dequeue().setTurno(true);
                contFila--;
                filaTexto.text = "" + contFila;
                cont++;
                aforo.text = "" + cont;
                StopCoroutine(checarCupo);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Persona")
        {
            cont--;
            aforo.text = "" + cont;
        }
    }

    public int getDuracion()
    {
        return this.duracion;
    }

    public int getIndice()
    {
        return this.indice;
    }
}

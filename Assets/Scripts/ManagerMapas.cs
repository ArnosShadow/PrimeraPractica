using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerMapas : MonoBehaviour
{
    [Header("Mapa")]
    [SerializeField] private GameObject[] fragmentosMapa;
    [SerializeField] private GameObject[] trampas;
    [SerializeField] private GameObject[] coleccionables; // Son las balas y las monedas
    [SerializeField] private GameObject checkpoint;
    [SerializeField] private GameObject mapaFinal;


    [SerializeField] private int trampasPorFila = 1;
    [SerializeField] private int coleccionablesPorMapa = 15;
    [SerializeField] private int mapasTotales = 5;
    private int mapasCont = 0;



    // Start is called before the first frame update
    void Start()
    {
        //Para instanciar tambien en el primer mapa
        if (transform.childCount > 0) // Verificamos si hay al menos un hijo
        {
            GameObject primerMapa = transform.GetChild(0).gameObject;

            if (primerMapa != null)
            {
                InstanciarEnMapa(primerMapa);
            }
        }
    }

    // Update is called once per frame
    public void GenerarMapa(Transform newSpawnPoint)
    {
        GameObject mapa;

        if (mapasCont < mapasTotales - 1)
        {
            // Generamos un fragmento normal o el túnel
            
                mapa = fragmentosMapa[Random.Range(0, fragmentosMapa.Length)];

        }
        else
        {
            // Si es el último mapa, generamos el mapa final
            mapa = mapaFinal;
        }

        GameObject mapaInstanciado = Instantiate(mapa, newSpawnPoint.position, newSpawnPoint.rotation);
        InstanciarEnMapa(mapaInstanciado);

        mapasCont++;
    }

    public void InstanciarEnMapa(GameObject mapa) {
        GenerarCheckpoint(mapa);
        GenerarTrampas(mapa);
        GenerarColeccionables(mapa);
    }

    private void GenerarCheckpoint(GameObject mapa)
    {
        Transform suelo = mapa.transform.Find("Suelo");

        if (suelo.childCount >= 3)
        {
            Transform filaCheckpoint = suelo.GetChild(2);

            Vector3 posicionLocal = filaCheckpoint.localPosition;

            Vector3 posicionFinal = mapa.transform.position + posicionLocal;

            posicionFinal.y = mapa.transform.position.y + 1.5f;

            GameObject nuevoCheckpoint = Instantiate(checkpoint, posicionFinal, Quaternion.identity, mapa.transform);

            PlayerMovement jugador = FindObjectOfType<PlayerMovement>();
            jugador.SetCheckpoint(nuevoCheckpoint.transform);

        }
    }

    private void GenerarColeccionables(GameObject mapa)
    {
        Transform suelo = mapa.transform.Find("Suelo");
        List<Transform> suelos= new List<Transform>();
        foreach (Transform fila in suelo)
        {
            if (fila.GetSiblingIndex() == 2) continue; 

            foreach (Transform emptySuelo in fila)
            {
                if (emptySuelo.childCount > 0)
                {
                    suelos.Add(emptySuelo.GetChild(0));
                }
            }
        }
        int coleccionablesEnEsteMapa = Random.Range(5, 10 + 1);


        List<int> i = new List<int>();
        while (i.Count < coleccionablesEnEsteMapa)
        {
            int ranI = Random.Range(0, suelos.Count);
            if (!i.Contains(ranI))
            {
                i.Add(ranI);
            }
        }

        foreach (int j in i)
        {
            Transform sueloSeleccionado = suelos[j];

            Renderer renderer = sueloSeleccionado.GetComponentInChildren<Renderer>();

            Vector3 posicionColeccionable =  renderer.bounds.center ;

            Instantiate(coleccionables[Random.Range(0, coleccionables.Length)],posicionColeccionable + Vector3.up * 0.5f, Quaternion.Euler(90f,0f,0f));

        }

    }

    public void GenerarTrampas(GameObject mapa) {

        // GameObject trampa = trampas[Random.Range(0, trampas.Length)];
        Transform suelo = mapa.transform.Find("Suelo");

        foreach (Transform fila in suelo) {

            if (fila.GetSiblingIndex() == 2) continue; 
            List<Transform> fragmentosSuelo = new List<Transform>();

            foreach (Transform fragmentoSuelo in fila) {

                fragmentosSuelo.Add(fragmentoSuelo.GetChild(0));
          
            }


            int trampasEnEstaFila = Random.Range(1, 3);

            List<int> i = new List<int>();

            while (i.Count < trampasEnEstaFila)
            {
                int randomIndex = Random.Range(0, fragmentosSuelo.Count);
                if (!i.Contains(randomIndex))
                {
                    i.Add(randomIndex);
                }
            }

            foreach (int j in i)
            {
                Transform moduloSuelo = fragmentosSuelo[j];
                Instantiate(trampas[Random.Range(0, trampas.Length)], moduloSuelo.position+new Vector3(0.5f,0.26f,-3.01f), moduloSuelo.rotation, mapa.transform);
                Destroy(moduloSuelo.gameObject, 0.1f);

            }
        }



    }
}

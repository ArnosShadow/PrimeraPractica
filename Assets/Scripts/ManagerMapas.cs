using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMapas : MonoBehaviour
{
    [Header("Mapa")]
    [SerializeField] private GameObject[] fragmentosMapa;
    [SerializeField] private GameObject[] trampas;

    [SerializeField] private int trampasPorFila = 1;
    private int filasGeneradas = 0;

    private int mapasCont = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void GenerarMapa(Transform newSpawnPoint)
    {
        GameObject mapa = fragmentosMapa[Random.Range(0, fragmentosMapa.Length)];
        
        Instantiate(mapa, newSpawnPoint.position, newSpawnPoint.rotation);

    }

    public void GenerarTrampas(Transform newSpawnTrampas) {
        
        
        
        
        
        GameObject trampa = trampas[Random.Range(0, trampas.Length)];








        Instantiate(trampa, newSpawnTrampas.position, newSpawnTrampas.rotation);

    }
}

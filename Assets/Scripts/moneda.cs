using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneda : MonoBehaviour
{
    [SerializeField] private float rotacion;
    [SerializeField] private float valor;
    public float Rotacion
    {
        get => rotacion;
        set => rotacion = value;
    }
    public float Monedas
    {
        get => valor;
        set => valor = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Space.World: Coordenadas globales
        //Space.Self: Coordenadas locales
        transform.Rotate(Vector3.up * rotacion * Time.deltaTime, Space.World);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIEstadisticas : MonoBehaviour
{
    [Header("Sadud")]
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private TMP_Text textoSalud;
    [SerializeField] private Slider barraSalud;

    [Header("Monedas")]
    [SerializeField] private TMP_Text textoMonedas;
    [SerializeField] private int cantidadMonedas;
    [Header("Municion")]
    [SerializeField] private TMP_Text textoMunicion;
    [SerializeField] private int cantidadBalas;



    private float vidaActual = 0f;

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMaxima;
        textoMunicion.SetText(cantidadBalas.ToString() + "/10");
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage) {
        vidaActual -= damage;

        if (vidaActual <= 0) {
            vidaActual = 100f;
        }

        textoSalud.SetText(vidaActual.ToString());
        barraSalud.value = vidaActual;
    }

    internal void TakeMoneda(int coin)
    {
        cantidadMonedas += coin;
        textoMonedas.SetText(cantidadMonedas.ToString());

    }

    internal void TakeMunicion(int bullet) {
        textoMunicion.SetText(bullet.ToString() + "/10");
    }
}

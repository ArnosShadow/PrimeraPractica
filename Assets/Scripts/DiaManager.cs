using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaManager : MonoBehaviour
{
    [Header("DiaNoche")]
    [SerializeField] private Light luzSolar;
    [SerializeField] private Material dia;
    [SerializeField] private Material noche;

    void Start()
    {
        Dia();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Noche();
        }
    }
    private void Dia() {
        RenderSettings.skybox = dia;
        luzSolar.intensity = 1f;
        luzSolar.transform.rotation = Quaternion.Euler(50f, 0, 0);

    }

    private void Noche() {
        RenderSettings.skybox = noche;
        luzSolar.intensity = 0.2f;
        luzSolar.transform.rotation = Quaternion.Euler(-50f, 0, 0);
    }
}

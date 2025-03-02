using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaManager : MonoBehaviour
{
    [Header("DiaNoche")]
    [SerializeField] private Light luzSolar;
    [SerializeField] private Material dia;
    [SerializeField] private Material noche;

    private bool esDeDia = true;
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
        esDeDia = true;
        RenderSettings.skybox = dia;
        luzSolar.intensity = 1f;
        luzSolar.transform.rotation = Quaternion.Euler(50f, 0, 0);

    }

    private void Noche() {
        esDeDia = false;
        RenderSettings.skybox = noche;
        luzSolar.intensity = 0.2f;
        luzSolar.transform.rotation = Quaternion.Euler(-50f, 0, 0);
    }
}

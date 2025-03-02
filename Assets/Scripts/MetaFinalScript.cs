using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject canvasJuegoGanado;

    private void Start()
    {
        canvasJuegoGanado.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canvasJuegoGanado != null)
            {
                canvasJuegoGanado.SetActive(true); 
            }
        }
    }
}

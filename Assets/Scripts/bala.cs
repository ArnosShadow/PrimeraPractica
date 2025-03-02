using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int cantidad = 3;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Trampa"))
        {
          
            Destroy(other.gameObject); // Destruir la trampa
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Trampa"))
        {

            Destroy(other.gameObject); // Destruir la trampa
        }
    }
    public int Bala
    {
        get => cantidad;
        set => cantidad = value;
    }
}

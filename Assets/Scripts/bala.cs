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
    public int Bala
    {
        get => cantidad;
        set => cantidad = value;
    }
}

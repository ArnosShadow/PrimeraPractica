using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampa : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float daño = 5;
    [SerializeField] private float veneno = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float Trampa
    {
        get => daño;
        set => daño = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaScript : MonoBehaviour
{
    public Transform player;
    public Transform emptyArma; 
    void Update()
    {

        emptyArma.position = player.position + new Vector3(0f, 0f, 0f);
        emptyArma.rotation = Quaternion.Euler(0f,0f,0f);


    }
}

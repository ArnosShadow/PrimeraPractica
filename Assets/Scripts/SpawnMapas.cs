using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapas : MonoBehaviour
{
    public GameObject nextFragmentPrefab; // Prefab del nuevo fragmento del mapa
    public Transform spawnPoint; // Punto donde aparecerá el fragmento
    private bool hasSpawned = false; // Evita que se generen fragmentos duplicados

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            Instantiate(nextFragmentPrefab, spawnPoint.position, spawnPoint.rotation);
            hasSpawned = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
  
        [SerializeField] private Renderer playerRenderer;
        [SerializeField] private Material[] materialesDisponibles;

        void Start()
        {
            string materialGuardado = PlayerPrefs.GetString("MaterialSeleccionado", "");
            Debug.Log("material" + materialGuardado);
            if (!string.IsNullOrEmpty(materialGuardado))
            {
                foreach (Material mat in materialesDisponibles)
                {
                Debug.Log("material" + materialGuardado + mat.name);
                if (mat.name == materialGuardado)
                    {
                        playerRenderer.material = mat;
                        break;
                    }
                }
            }
        }
    
}

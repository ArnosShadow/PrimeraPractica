using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private Material[] materialesDisponibles; 

   
    //Método asociado al click del boton de Inicio
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(0);

    }
    public void OnWinButtonClicked() {
        SceneManager.LoadScene(1);

    }
    public void OnTiendaButtonClicked()
    {
        panelTienda.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnObjetoButtonClicked(int index)
    {
        if (index >= 0 && index < materialesDisponibles.Length)
        {
            string nombreMaterial = materialesDisponibles[index].name;
            PlayerPrefs.SetString("MaterialSeleccionado", nombreMaterial);
            PlayerPrefs.Save();
        }
    }
}
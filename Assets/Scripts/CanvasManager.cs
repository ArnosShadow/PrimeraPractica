using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private GameObject panelSalir;
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
        panelSalir.SetActive(false);
    }
    public void OnSalirButtonClicked()
    {
        panelTienda.SetActive(false);
        panelSalir.SetActive(true);
    }

    public void OnSeleccionarButtonClicked()
    {
        //Selecciona el material para el jugador
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
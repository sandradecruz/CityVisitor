using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CambiarIdioma : MonoBehaviour
{
    [Header("Configuración del Botón")]
    public Image imagenBoton; // El componente Image del botón b_Idioma
    public Sprite banderaEsp; // Sprite de España
    public Sprite banderaEng; // Sprite de Reino Unido


    public void AlternarIdioma()
    {
        int nuevoIdioma = (PlayerPrefs.GetInt("IdiomaSeleccionado", 0) == 0) ? 1 : 0;
        PlayerPrefs.SetInt("IdiomaSeleccionado", nuevoIdioma);
        PlayerPrefs.Save();

        ActualizarTodaLaEscena();
    }

    public void ActualizarTodaLaEscena()
    {
        // Cambiar la bandera
        int idioma = PlayerPrefs.GetInt("IdiomaSeleccionado", 0);
        if (imagenBoton != null)
            imagenBoton.sprite = (idioma == 0) ? banderaEsp : banderaEng;

        // BUSCAR TODOS los textos que tengan el script TextoLocalizable y actualizarlos
        TextoLocalizable[] todosLosTextos = FindObjectsByType<TextoLocalizable>(FindObjectsSortMode.None);
        foreach (TextoLocalizable t in todosLosTextos)
        {
            t.ActualizarIdioma();
        }
        ControladorDialogoSimple dialogoPaginado = FindFirstObjectByType<ControladorDialogoSimple>();
        if (dialogoPaginado != null)
        {
            dialogoPaginado.ActualizarIdioma();
        }
    }

    void Start()
    {
        ActualizarTodaLaEscena();
    }
}
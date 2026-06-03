using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorDialogoSimple : MonoBehaviour
{
    [Header("Lista de Páginas")]
    [SerializeField] public List<PaginaTexto> paginas;
    private int indiceActual = 0;

    [Header("Componentes de la UI")]
    [SerializeField] public TextMeshProUGUI componenteTexto;
    [SerializeField] public Button botonAtras;
    [SerializeField] public Button botonSiguiente;

    void Start()
    {
        // Asignar funciones a las flechas del Canvas
        botonSiguiente.onClick.AddListener(AvanzarPagina);
        botonAtras.onClick.AddListener(RetrocederPagina);

        // Forzar la primera carga con el idioma correcto
        ActualizarIdioma();
    }

    // Tu script CambiarIdioma llamará automáticamente a esta función "mid-game"
    public void ActualizarIdioma()
    {
        if (paginas == null || paginas.Count == 0) return;

        PaginaTexto paginaActual = paginas[indiceActual];

        // Leemos el idioma directamente de PlayerPrefs (0 = Español, 1 = Inglés)
        int idiomaDetectado = PlayerPrefs.GetInt("IdiomaSeleccionado", 0);

        if (idiomaDetectado == 0)
        {
            componenteTexto.text = paginaActual.textoEspanol;
        }
        else
        {
            componenteTexto.text = paginaActual.textoIngles;
        }

        // Controlar el estado de las flechas interactuables
        botonAtras.interactable = (indiceActual > 0);
        botonSiguiente.interactable = (indiceActual < paginas.Count - 1);
    }

    void AvanzarPagina()
    {
        if (indiceActual < paginas.Count - 1)
        {
            indiceActual++;
            ActualizarIdioma(); // Actualiza al avanzar
        }
    }

    void RetrocederPagina()
    {
        if (indiceActual > 0)
        {
            indiceActual--;
            ActualizarIdioma(); // Actualiza al retroceder
        }
    }
}

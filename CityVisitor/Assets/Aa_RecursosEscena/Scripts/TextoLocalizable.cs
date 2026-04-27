using UnityEngine;
using TMPro;

public class TextoLocalizable : MonoBehaviour
{
    public string textoEspanol;
    public string textoIngles;
    private TextMeshProUGUI componenteTexto;

    void Awake()
    {
        componenteTexto = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        // Se actualiza automáticamente cuando aparece el texto o cambia la escena
        ActualizarIdioma();
    }

    public void ActualizarIdioma()
    {
        int idioma = PlayerPrefs.GetInt("IdiomaSeleccionado", 0);
        if (componenteTexto != null)
        {
            componenteTexto.text = (idioma == 0) ? textoEspanol : textoIngles;
        }
    }
}

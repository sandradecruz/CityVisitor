using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Esto asegura que el GameObject tenga un AudioSource y evita errores
[RequireComponent(typeof(AudioSource))]
public class AccionAudio : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Configuracion de la Barra de Carga")]
    public Image barraDeCarga;
    public GameObject canvasDialogo;

    [Header("Configuracion de Audio")]
    public AudioClip vozEspanol;
    public AudioClip vozIngles;

    private float tiempoRequerido = 0.5f;
    private float tiempoActual = 0f;
    private bool estaPulsando = false;

    // Referencia privada al componente de audio del objeto
    private AudioSource miAudioSource;

    void Awake()
    {
        // Obtenemos el componente AudioSource al iniciar
        miAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (estaPulsando)
        {
            tiempoActual += Time.deltaTime;
            barraDeCarga.fillAmount = tiempoActual / tiempoRequerido;

            if (tiempoActual >= tiempoRequerido)
            {
                estaPulsando = false;
                MostrarCanvasYVoz();
            }
        }
        else
        {
            tiempoActual = Mathf.Max(0, tiempoActual - Time.deltaTime * 2);
            barraDeCarga.fillAmount = tiempoActual / tiempoRequerido;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        estaPulsando = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        estaPulsando = false;
    }

    void MostrarCanvasYVoz()
    {
        // Reseteamos la barra
        tiempoActual = 0f;
        if (barraDeCarga != null) barraDeCarga.fillAmount = 0f;

        // 1. Activamos el Canvas de texto
        if (canvasDialogo != null)
        {
            canvasDialogo.SetActive(true);
        }
        
        // 2. Control del Audio corregido
        if (miAudioSource.isPlaying)
        {
            // Si ya estaba sonando, lo detenemos
            miAudioSource.Stop();
        }
        else
        {
            // Comprobamos idioma
            int idioma = PlayerPrefs.GetInt("IdiomaSeleccionado", 0);
            AudioClip clipA_Reproducir = (idioma == 0) ? vozEspanol : vozIngles;

            Debug.Log("Idioma: " + idioma + " | Reproduciendo: " + (clipA_Reproducir != null ? clipA_Reproducir.name : "Ninguno"));

            if (clipA_Reproducir != null)
            {
                // Asignamos el clip al componente y lo reproducimos
                miAudioSource.clip = clipA_Reproducir;
                miAudioSource.Play();
            }
        }
    }

    void OnDisable()
    {
        estaPulsando = false;
        tiempoActual = 0f;
        if (barraDeCarga != null) barraDeCarga.fillAmount = 0f;
    }
}
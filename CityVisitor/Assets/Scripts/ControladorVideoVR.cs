using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR; 
using System.Collections.Generic;

public class ControladorVideoVR : MonoBehaviour
{
    [Header("Componentes de Video")]
    public VideoPlayer videoPlayer;
    public Material materialVideoSkybox;

    [Header("Objetos del Mundo Real")]
    [Tooltip("Arrastra aquí el objeto padre de tu sala para ocultarla durante el video")]
    public GameObject habitacionMundoReal;
    
    [Tooltip("Arrastra aquí la esfera que el jugador toca para que desaparezca")]
    public GameObject esferaActivadora; // <--- NUEVO

    [Header("Movimiento y Físicas del Jugador")]
    public MonoBehaviour componenteMovimiento;
    public Transform jugador;

    private Material skyboxOriginal;
    private bool estaReproduciendo = false;
    private bool botonPresionadoEnFrameAnterior = false;

    private Vector3 posicionInicialJugador;
    private Rigidbody rbJugador;
    private bool gravedadOriginal;
    private bool kinematicOriginal;

    void Start()
    {
        skyboxOriginal = RenderSettings.skybox;
        videoPlayer.loopPointReached += AlTerminarVideo;
        videoPlayer.renderMode = VideoRenderMode.APIOnly;

        if (jugador != null)
        {
            rbJugador = jugador.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (!estaReproduciendo) return;

        if (videoPlayer.texture != null)
        {
            materialVideoSkybox.mainTexture = videoPlayer.texture;
        }

        // BLOQUEO DE EJE Y: Mantiene la altura exacta del jugador
        if (jugador != null)
        {
            jugador.position = new Vector3(jugador.position.x, posicionInicialJugador.y, jugador.position.z);
        }

        DetectarBotonAVR();
    }

    void DetectarBotonAVR()
    {
        var dispositivosMandoDerecho = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, dispositivosMandoDerecho);

        if (dispositivosMandoDerecho.Count > 0)
        {
            InputDevice mandoDerecho = dispositivosMandoDerecho[0];
            if (mandoDerecho.TryGetFeatureValue(CommonUsages.primaryButton, out bool botonAPresionado))
            {
                if (botonAPresionado && !botonPresionadoEnFrameAnterior)
                {
                    CambiarPausa();
                }
                botonPresionadoEnFrameAnterior = botonAPresionado;
            }
        }
    }

    public void ActivarVideoVR()
    {
        estaReproduciendo = true;

        if (jugador != null)
        {
            posicionInicialJugador = jugador.position;

            if (rbJugador != null)
            {
                gravedadOriginal = rbJugador.useGravity;
                kinematicOriginal = rbJugador.isKinematic;

                rbJugador.useGravity = false;
                rbJugador.isKinematic = true; 
            }
        }

        // 1. OCULTAMOS LA SALA Y LA ESFERA
        if (habitacionMundoReal != null)
        {
            habitacionMundoReal.SetActive(false);
        }
        
        if (esferaActivadora != null)
        {
            esferaActivadora.SetActive(false); // Apagamos la pelota
        }

        // 2. CONGELAMOS AL JUGADOR
        if (componenteMovimiento != null)
        {
            componenteMovimiento.enabled = false;
        }

        // 3. ARRANCAMOS EL VIDEO
        RenderSettings.skybox = materialVideoSkybox;
        DynamicGI.UpdateEnvironment();
        videoPlayer.Play();
    }

    void CambiarPausa()
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
        else
            videoPlayer.Play();
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        RegresarAlMundo();
    }

    void RegresarAlMundo()
    {
        estaReproduciendo = false;
        videoPlayer.Stop();
        materialVideoSkybox.mainTexture = null;

        // 1. RESTAURAMOS EL CIELO
        RenderSettings.skybox = skyboxOriginal;
        DynamicGI.UpdateEnvironment();

        // 2. MOSTRAMOS LA SALA Y LA ESFERA DE NUEVO
        if (habitacionMundoReal != null)
        {
            habitacionMundoReal.SetActive(true);
        }
        
        if (esferaActivadora != null)
        {
            esferaActivadora.SetActive(true); // Encendemos la pelota
        }

        // Restauramos físicas del jugador
        if (jugador != null)
        {
            jugador.position = posicionInicialJugador;

            if (rbJugador != null)
            {
                rbJugador.useGravity = gravedadOriginal;
                rbJugador.isKinematic = kinematicOriginal;
            }
        }

        // 3. DESCONGELAMOS AL JUGADOR
        if (componenteMovimiento != null)
        {
            componenteMovimiento.enabled = true;
        }
    }
}
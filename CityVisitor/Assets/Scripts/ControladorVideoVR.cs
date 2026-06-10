using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem; 

public class ControladorVideoVR : MonoBehaviour
{
    [Header("Controles VR (Asignar en Inspector)")]
    [Tooltip("Arrastra aquí la acción del Botón A (Ej: XRI RightHand Interaction/Primary Button)")]
    public InputActionReference botonA_Action;
    
    [Tooltip("Arrastra aquí la acción del Joystick Izquierdo (Ej: XRI LeftHand Locomotion/Move)")]
    public InputActionReference joystickIzquierdo_Action;

    [Header("Componentes de Video")]
    public VideoPlayer videoPlayer;
    public Material materialVideoSkybox;

    [Header("Objetos del Mundo Real")]
    public GameObject habitacionMundoReal;
    public GameObject esferaActivadora; 

    [Header("Movimiento y Físicas del Jugador")]
    public MonoBehaviour componenteMovimiento;
    public Transform jugador;

    private Material skyboxOriginal;
    private bool estaReproduciendo = false;
    private bool estaPausadoManualmente = false;

    private Vector3 posicionInicialJugador;
    private Rigidbody rbJugador;
    private bool gravedadOriginal;
    private bool kinematicOriginal;

    // Variables para controlar el retroceso fluido
    private float temporizadorRetroceso = 0f;
    private const float INTERVALO_RETROCESO = 0.01f; // Cada cuántos segundos reales se hace un salto hacia atrás
    private const double TIEMPO_A_RETROCEDER = 1;   // Cuántos segundos de video se salta hacia atrás en cada intervalo

   
    private void OnEnable()
    {
        if (botonA_Action != null)
        {
            botonA_Action.action.Enable();
            botonA_Action.action.performed += BotonAPulsado; 
        }

        if (joystickIzquierdo_Action != null)
        {
            joystickIzquierdo_Action.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (botonA_Action != null)
        {
            botonA_Action.action.performed -= BotonAPulsado;
            botonA_Action.action.Disable();
        }

        if (joystickIzquierdo_Action != null)
        {
            joystickIzquierdo_Action.action.Disable();
        }
    }

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

        // Bloquea el eje Y del jugador
        if (jugador != null)
        {
            jugador.position = new Vector3(jugador.position.x, posicionInicialJugador.y, jugador.position.z);
        }

        // Procesar Joystick solo si no está pausado
        if (!estaPausadoManualmente)
        {
            ProcesarJoystick();
        }
    }

    // cuando pulsas "A"
    private void BotonAPulsado(InputAction.CallbackContext context)
    {
        if (!estaReproduciendo) return;

        if (videoPlayer.isPlaying && !estaPausadoManualmente)
        {
            videoPlayer.Pause();
            estaPausadoManualmente = true;
        }
        else
        {
            videoPlayer.playbackSpeed = 1.0f;
            videoPlayer.Play();
            estaPausadoManualmente = false;
        }
    }

    private void ProcesarJoystick()
    {
        if (joystickIzquierdo_Action == null) return;

        
        Vector2 joystickEje = joystickIzquierdo_Action.action.ReadValue<Vector2>();
        float umbral = 0.4f;

        // joystick izquierda -- Avanzar más rápido (1.5x)
        if (joystickEje.x < -umbral) 
        {
            videoPlayer.playbackSpeed = 1.5f;
            temporizadorRetroceso = 0f; 
        }
        // joystick derecha -- Retroceder a velocidad normal de forma limpia
        else if (joystickEje.x > umbral) 
        {
            // Pausamos momentáneamente la reproducción normal para que no compita con el salto temporal
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }

            temporizadorRetroceso += Time.deltaTime;

            // Cada vez que se cumple el intervalo, retrasamos el frame
            if (temporizadorRetroceso >= INTERVALO_RETROCESO)
            {
                temporizadorRetroceso = 0f;

                if (videoPlayer.time - TIEMPO_A_RETROCEDER > 0)
                {
                    videoPlayer.time -= TIEMPO_A_RETROCEDER;
                }
                else
                {
                    videoPlayer.time = 0;
                }
            }
        }
        // joystick centro -- Velocidad Normal
        else 
        {
            temporizadorRetroceso = 0f;

            // Si veníamos de retroceder, el video estará pausado, así que lo reanudamos
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
            }

            if (videoPlayer.playbackSpeed != 1.0f)
            {
                videoPlayer.playbackSpeed = 1.0f;
            }
        }
    }

    public void ActivarVideoVR()
    {
        estaReproduciendo = true;
        estaPausadoManualmente = false;
        temporizadorRetroceso = 0f;

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

        if (habitacionMundoReal != null) habitacionMundoReal.SetActive(false);
        if (esferaActivadora != null) esferaActivadora.SetActive(false); 
        if (componenteMovimiento != null) componenteMovimiento.enabled = false;

        RenderSettings.skybox = materialVideoSkybox;
        DynamicGI.UpdateEnvironment();
        videoPlayer.playbackSpeed = 1.0f;
        videoPlayer.Play();
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        RegresarAlMundo();
    }

    void RegresarAlMundo()
    {
        estaReproduciendo = false;
        estaPausadoManualmente = false;
        videoPlayer.Stop();
        materialVideoSkybox.mainTexture = null;
        
        RenderSettings.skybox = skyboxOriginal;
        DynamicGI.UpdateEnvironment();

        if (habitacionMundoReal != null) habitacionMundoReal.SetActive(true);
        if (esferaActivadora != null) esferaActivadora.SetActive(true); 

        if (jugador != null)
        {
            jugador.position = posicionInicialJugador;
            if (rbJugador != null)
            {
                rbJugador.useGravity = gravedadOriginal;
                rbJugador.isKinematic = kinematicOriginal;
            }
        }
        
        if (componenteMovimiento != null) componenteMovimiento.enabled = true;
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [Header("Interfaz Visual")]
    public GameObject pauseMenu;

    [Space(10)]
    [Header("Configuración VR (Usar botón menú mando Izquierdo)")]
    public InputActionProperty botonMenuVR;

    void Start()
    {
        // El menú empieza oculto al arrancar el juego
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    private void OnEnable()
    {
        // Activamos la acción y nos suscribimos de forma limpia al evento de pulsación
        botonMenuVR.action.Enable();
        botonMenuVR.action.performed += OnMenuButtonPressed;
    }

    private void OnDisable()
    {
        // Cancelamos la suscripción de forma segura para evitar errores en Unity
        botonMenuVR.action.performed -= OnMenuButtonPressed;
        botonMenuVR.action.Disable();
    }

    void Update()
    {
        // Mantener la compatibilidad con el teclado del PC (Tecla Escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            abrirMenu();
        }
    }

    // Este método escucha el evento del Input System de las gafas VR
    private void OnMenuButtonPressed(InputAction.CallbackContext context)
    {
        abrirMenu();
    }

    // Tu función principal que alterna el estado del menú y pausa el tiempo
    public void abrirMenu()
    {
        if (pauseMenu == null) return;

        pauseMenu.SetActive(!pauseMenu.activeSelf);

        // Si el menú de pausa está activo, congelamos el juego (TimeScale = 0)
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // Funciones para cambiar de escena
    public void CambiarValladolid()
    {
        Time.timeScale = 1f; // Asegura que el tiempo vuelva a la normalidad al cambiar de mapa
        UnityEngine.SceneManagement.SceneManager.LoadScene("SceneValladolid");
    }

    public void CambiarSalamanca()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("SceneSalamanca");
    }

    public void CambiarMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Escena_MenuPrincipal");
    }
}
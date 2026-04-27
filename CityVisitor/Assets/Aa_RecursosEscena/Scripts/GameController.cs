using UnityEngine;
using UnityEngine.InputSystem; 

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;

    [Space(10)]
    [Header("Configuración VR")]
    public InputActionProperty botonMenuDerecho;


    void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        botonMenuDerecho.action.Enable();
        botonMenuDerecho.action.performed += _ => abrirMenu();
    }

    private void OnDisable()
    {
        botonMenuDerecho.action.performed -= _ => abrirMenu();
        botonMenuDerecho.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando pulsemos el boton escape, abrimos el menu de pausa 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            //Si el menu de pausa esta activo, pausamos el juego
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
    public void abrirMenu()
    {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            //Si el menu de pausa esta activo, pausamos el juego
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        
    }

    public void CambiarValladolid()
    {
                UnityEngine.SceneManagement.SceneManager.LoadScene("SceneValladolid");
    }

    public void CambiarSalamanca()
    {
                UnityEngine.SceneManagement.SceneManager.LoadScene("SceneSalamanca");
    }

    public void CambiarMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Escena_MenuPrincipal");
    }
}

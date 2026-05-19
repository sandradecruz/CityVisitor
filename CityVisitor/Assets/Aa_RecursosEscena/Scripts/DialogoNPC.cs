using UnityEngine;
using UnityEngine.InputSystem; // Necesario para el nuevo Input System de XR

public class DialogoNPC : MonoBehaviour
{
    [Header("Input de VR (Botón B - Mano Derecha)")]
    // Cambia a modo "Use Reference" en el inspector y asigna el secondaryButton de la mano derecha
    [SerializeField] private InputActionProperty botonCerrarInput;

    private bool dialogoActivo = false;

    void Start()
    {
        // Aseguramos que el diálogo esté oculto al inicio
        gameObject.SetActive(false);
    }


    // Se ejecuta automáticamente en cuanto el script de la barra de carga hace el SetActive(true)
    void OnEnable()
    {
        dialogoActivo = true;

        // Nos suscribimos al evento de pulsar el botón B
        if (botonCerrarInput.action != null)
        {
            botonCerrarInput.action.started += IntentarCerrarDialogo;
        }
    }

    void OnDisable()
    {
        // Nos desuscribimos al apagar el Canvas para evitar errores de memoria
        if (botonCerrarInput.action != null)
        {
            botonCerrarInput.action.started -= IntentarCerrarDialogo;
        }
    }

    private void IntentarCerrarDialogo(InputAction.CallbackContext context)
    {
        // Si por algún motivo el diálogo no está activo, ignoramos la pulsación del botón B
        if (!dialogoActivo) return;

        CerrarDialogo();
    }

    public void CerrarDialogo()
    {
        dialogoActivo = false;

        // Apaga el propio Canvas donde está metido este script, ocultándolo por completo
        gameObject.SetActive(false);
    }
}
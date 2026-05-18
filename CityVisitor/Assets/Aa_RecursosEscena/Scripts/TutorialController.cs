using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Header("Configuración del XR Rig")]
    [Tooltip("Arrastra aquí tu XR Origin o XR Rig")]
    public GameObject xrRig;

    // Guardamos la posición de destino en una variable
    private Vector3 targetPosition = new Vector3(-18f, 0f, 0f);
    private Vector3 homePosition = new Vector3(0f, 0f, 0f);
    public void TeleportToTutorial()
    {
        if (xrRig != null)
        {
            // Teletransportamos el XR Rig a las coordenadas deseadas
            xrRig.transform.position = targetPosition;

            Debug.Log($"XR Rig teletransportado con éxito a: {targetPosition}");
        }
        else
        {
            Debug.LogWarning("Por favor, asigna el objeto XR Rig en el Inspector.");
        }
    }
    public void TeleportBackToHome()
    {
        MoveRig(homePosition);
    }

    // Método interno para evitar repetir código
    private void MoveRig(Vector3 destination)
    {
        if (xrRig != null)
        {
            xrRig.transform.position = destination;
            Debug.Log($"XR Rig movido a: {destination}");
        }
        else
        {
            Debug.LogWarning("Por favor, asigna el objeto XR Rig en el Inspector.");
        }
    }
}

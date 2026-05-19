using UnityEngine;

public class DetectorProximidad : MonoBehaviour
{
    public GameObject panelUI; // Arrastra aquí tu Canvas flotante

    void Start()
    {
        if (panelUI != null) panelUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprobamos si lo que entró es el jugador (el XR Origin o la cámara)
        if (other.CompareTag("XR Origin (XR Rig) Variant") || other.gameObject.name.Contains("XR Origin"))
        {
            panelUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.name.Contains("XR"))
        {
            panelUI.SetActive(false);
            // Aquí también deberías resetear la barra de carga si el jugador se va a mitad del proceso
        }
    }
}
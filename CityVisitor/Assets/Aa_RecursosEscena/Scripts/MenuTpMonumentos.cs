using UnityEngine;

public class TeletransporteMonumentos : MonoBehaviour
{
    [Header("Referencia del Jugador")]
    public GameObject xrOrigin;

    [Header("Puntos de Teletransporte (Transforms)")]
    public Transform puntoCaballeria;

    public Transform puntoSanFrancisco;

    public Transform puntoSanPablo;

    public Transform puntoUniversidad;

    public Transform puntoSanBenito;
    
    public Transform puntoVeraCruz;

    public void TeletransportarACaballeria()
    {
        MoverJugador(puntoCaballeria, "Caballería");
    }

    public void TeletransportarASanFrancisco()
    {
        MoverJugador(puntoSanFrancisco, "San Francisco");
    }

    public void TeletransportarASanPablo()
    {
        MoverJugador(puntoSanPablo, "San Pablo");
    }

    public void TeletransportarAUniversidad()
    {
        MoverJugador(puntoUniversidad, "Universidad");
    }

    public void TeletransportarASanBenito()
    {
        MoverJugador(puntoSanBenito, "San Benito");
    }

    public void TeletransportarAVeraCruz()
    {
        MoverJugador(puntoVeraCruz, "Vera Cruz");
    }

    private void MoverJugador(Transform puntoDestino, string nombreMonumento)
    {
        if (xrOrigin == null)
        {
            Debug.LogError("[Teletransporte] ¡No has asignado el XR Origin / XR Rig en el Inspector!");
            return;
        }

        if (puntoDestino == null)
        {
            Debug.LogWarning($"[Teletransporte] No se ha asignado el punto de destino para {nombreMonumento}.");
            return;
        }

        CharacterController controller = xrOrigin.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;

        // Teletransportamos la posición y la rotación
        xrOrigin.transform.position = puntoDestino.position;
        xrOrigin.transform.rotation = puntoDestino.rotation;

        if (controller != null) controller.enabled = true;

    }
}
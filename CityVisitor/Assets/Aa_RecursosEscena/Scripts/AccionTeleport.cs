using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Para cambiar de escena
using UnityEngine.EventSystems;    // Para detectar si mantienes pulsado el láser

public class AccionTeleport : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image barraDeCarga; // Tu imagen con Fill Method: Horizontal
    public string nombreEscenaDestino; // Nombre exacto de la escena a la que vas

    private float tiempoRequerido = 1.5f; // 2 segundos manteniendo
    private float tiempoActual = 0f;
    private bool estaPulsando = false;

    void Update()
    {
        if (estaPulsando)
        {
            tiempoActual += Time.deltaTime;
            barraDeCarga.fillAmount = tiempoActual / tiempoRequerido;

            if (tiempoActual >= tiempoRequerido)
            {
                estaPulsando = false; // Evita bucles
                Teletransportar();
            }
        }
        else
        {
            // Si suelta el botón, la barra se vacía
            tiempoActual = Mathf.Max(0, tiempoActual - Time.deltaTime * 2);
            barraDeCarga.fillAmount = tiempoActual / tiempoRequerido;
        }
    }

    // El láser del mando VR pulsa el botón
    public void OnPointerDown(PointerEventData eventData)
    {
        estaPulsando = true;
    }

    // El jugador suelta el gatillo del mando VR
    public void OnPointerUp(PointerEventData eventData)
    {
        estaPulsando = false;
    }

    void Teletransportar()
    {
        Debug.Log("Cambiando a escena: " + nombreEscenaDestino);
        SceneManager.LoadScene(nombreEscenaDestino);
    }

    // Por seguridad, si el panel se apaga porque el jugador se aleja, reseteamos todo
    void OnDisable()
    {
        estaPulsando = false;
        tiempoActual = 0f;
        if (barraDeCarga != null) barraDeCarga.fillAmount = 0f;
    }
}
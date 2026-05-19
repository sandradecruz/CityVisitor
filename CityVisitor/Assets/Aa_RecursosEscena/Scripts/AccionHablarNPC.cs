using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;    // Para detectar si mantienes pulsado el láser

public class AccionHablarNPC : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image barraDeCarga; // Tu imagen con Fill Method: Horizontal
    public GameObject canvasDialogo; // Arrastra aquí el objeto del Canvas de Diálogo

    private float tiempoRequerido = 1.5f; // 1.5 segundos manteniendo
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
                MostrarCanvas();
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

    void MostrarCanvas()
    {
        // Reseteamos la barra
        tiempoActual = 0f;
        if (barraDeCarga != null) barraDeCarga.fillAmount = 0f;

        // Hace visible el recuadro de chat directamente
        if (canvasDialogo != null)
        {
            canvasDialogo.SetActive(true);
        }
    }

    // Por seguridad, si el panel se apaga porque el jugador se aleja, reseteamos todo
    void OnDisable()
    {
        estaPulsando = false;
        tiempoActual = 0f;
        if (barraDeCarga != null) barraDeCarga.fillAmount = 0f;
    }
}
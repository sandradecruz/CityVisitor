using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AccionHablarNPC : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image barraDeCarga;
    public GameObject canvasDialogo;

    public float tiempoRequerido = 0.5f;
    private float tiempoActual = 0f;
    private bool estaPulsando = false;

    void Awake()
    {
        if (canvasDialogo != null)
        {
            canvasDialogo.SetActive(false);
        }
    }
    void Update()
    {
        if (estaPulsando)
        {
            tiempoActual += Time.deltaTime;
            barraDeCarga.fillAmount = tiempoActual / tiempoRequerido;

            if (tiempoActual >= tiempoRequerido)
            {
                estaPulsando = false; 
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

    public void OnPointerDown(PointerEventData eventData)
    {
        estaPulsando = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        estaPulsando = false;
    }

    void MostrarCanvas()
    {
        // Reseteamos la barra
        tiempoActual = 0f;
        if (barraDeCarga != null) barraDeCarga.fillAmount = 0f;

        if (canvasDialogo != null)
        {
            canvasDialogo.SetActive(true);
        }
    }

    void OnDisable()
    {
        estaPulsando = false;
        tiempoActual = 0f;
        if (barraDeCarga != null) barraDeCarga.fillAmount = 0f;
    }
}
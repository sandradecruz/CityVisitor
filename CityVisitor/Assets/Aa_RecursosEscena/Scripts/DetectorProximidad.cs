using UnityEngine;
using WaypointsFree;

public class DetectorProximidad : MonoBehaviour
{
    public GameObject panelUI; // Arrastra aquí tu Canvas flotante

    private WaypointsTraveler traveler; // Referencia al componente del asset
    private Transform playerTransform; // Para guardar la posición del jugador
    private bool isTalking = false;


    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null) animator = GetComponentInParent<Animator>();

        traveler = GetComponent<WaypointsTraveler>();
        if (traveler == null)
        {
            traveler = GetComponentInParent<WaypointsTraveler>();
        }

        if (traveler == null)
        {
            Debug.Log($"[DetectorProximidad] El objeto {gameObject.name} no tiene WaypointTraveler. Solo se activará el UI.");
        }
    }

    void Start()
    {
        if (panelUI != null) panelUI.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        // Comprobamos si lo que entró es el jugador
        if (other.CompareTag("XR Origin (XR Rig) Variant") || other.gameObject.name.Contains("XR Origin"))
        {
            panelUI.SetActive(true);

            playerTransform = other.transform;
            isTalking = true;

            if (traveler != null)
            {
                traveler.Move(false);
            }
            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.name.Contains("XR"))
        {
            panelUI.SetActive(false);

            isTalking = false;
            if (traveler != null)
            {
                traveler.Move(true);
                if (animator != null)
                {
                    animator.SetFloat("Speed", 1f);
                }
            }

            playerTransform = null;
        }
    }
}
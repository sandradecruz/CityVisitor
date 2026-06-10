using UnityEngine;
using TMPro; 
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NPCQuest : MonoBehaviour
{
    [Header("UI Component")]
    [Tooltip("Arrastra aquí el componente TextMeshProUGUI de tu único Canvas")]
    public TextMeshProUGUI npcText; 

    [Header("Texts - Pidiendo")]
    [TextArea(2, 4)] public string textoPidiendoEspanol = "Se me han caído mis manzanas, ¿me ayudas?";
    [TextArea(2, 4)] public string textoPidiendoIngles = "My apples fell, can you help me?";

    [Header("Texts - Completado")]
    [TextArea(2, 4)] public string textoCompletadoEspanol = "Gracias por recoger todas mis manzanas";
    [TextArea(2, 4)] public string textoCompletadoIngles = "Thank you for gathering all my apples.";

    [Header("Quest Settings")]
    public int targetApples = 10;
    private int applesDelivered = 0;
    private bool isQuestCompleted = false;

    [Header("Hand Configuration")]
    [Tooltip("Arrastra aquí el GameObject de tu mando derecho (ej. Right Controller)")]
    public GameObject mandoDerecho; 

    private XRSimpleInteractable simpleInteractable;
    
    //  Guarda el último idioma detectado para saber cuándo cambia
    private int ultimoIdiomaDetectado = -1; 

    void Start()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();
        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.AddListener(OnNPCSelected);
        }

        ActualizarTextoNPC();
    }

    // Escucha constantemente si el jugador toca el menú de idioma
    void Update()
    {
        int idiomaActual = PlayerPrefs.GetInt("IdiomaSeleccionado", 0);

        // Si el idioma del menú es diferente al que teníamos guardado, actualizamos
        if (idiomaActual != ultimoIdiomaDetectado)
        {
            ultimoIdiomaDetectado = idiomaActual;
            ActualizarTextoNPC();
        }
    }

    void OnNPCSelected(SelectEnterEventArgs args)
    {
        if (isQuestCompleted) return;

        GameObject interactorGO = args.interactorObject.transform.gameObject;
        if (mandoDerecho != null && interactorGO != mandoDerecho && !interactorGO.transform.IsChildOf(mandoDerecho.transform))
        {
            Debug.Log($"Interacción ignorada: El objeto '{interactorGO.name}' no pertenece al mando derecho.");
            return; 
        }

        if (PlayerInventory.Instance != null)
        {
            int playerApples = PlayerInventory.Instance.appleCount;

            if (playerApples > 0)
            {
                applesDelivered += playerApples;
                
                PlayerInventory.Instance.appleCount = 0;
                PlayerInventory.Instance.ClearCollectedApples();

                Debug.Log($"Entregadas {playerApples} manzanas. Total: {applesDelivered}/{targetApples}");

                if (applesDelivered >= targetApples)
                {
                    CompleteQuest();
                }
            }
        }
    }

    void CompleteQuest()
    {
        isQuestCompleted = true;
        
        ActualizarTextoNPC();
        
        if (PlayerInventory.Instance != null && PlayerInventory.Instance.appleText != null)
        {
            PlayerInventory.Instance.appleText.gameObject.SetActive(false);
        }

        Debug.Log("¡Misión de manzanas completada!");
    }

    void OnEnable()
    {
       ActualizarTextoNPC();
    }

    public void ActualizarTextoNPC()
    {
        if (npcText == null) return;

        int idioma = PlayerPrefs.GetInt("IdiomaSeleccionado", 0); 

        if (!isQuestCompleted)
        {
            npcText.text = (idioma == 0) ? textoPidiendoEspanol : textoPidiendoIngles;
        }
        else
        {
            npcText.text = (idioma == 0) ? textoCompletadoEspanol : textoCompletadoIngles;
        }
    }

    private void OnDestroy()
    {
        if (simpleInteractable != null)
            simpleInteractable.selectEntered.RemoveListener(OnNPCSelected);
    }
}
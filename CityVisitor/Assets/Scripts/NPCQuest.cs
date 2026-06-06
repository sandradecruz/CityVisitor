using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NPCQuest : MonoBehaviour
{
    [Header("UI Canvases")]
    public GameObject canvasPidiendo;
    public GameObject canvasCompletado;

    [Header("Quest Settings")]
    public int targetApples = 10;
    private int applesDelivered = 0;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable simpleInteractable;

    void Start()
    {
        simpleInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.AddListener(OnNPCSelected);
        }

        if (canvasPidiendo != null) canvasPidiendo.SetActive(true);
        if (canvasCompletado != null) canvasCompletado.SetActive(false);
    }

    void OnNPCSelected(SelectEnterEventArgs args)
    {
        
        if (applesDelivered >= targetApples) return;

        if (PlayerInventory.Instance != null)
        {
            int playerApples = PlayerInventory.Instance.appleCount;

            if (playerApples > 0)
            {
                
                applesDelivered += playerApples;
                
                
                PlayerInventory.Instance.appleCount = 0;

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
       
        if (canvasPidiendo != null) canvasPidiendo.SetActive(false);
        if (canvasCompletado != null) canvasCompletado.SetActive(true);
        
        
        if (PlayerInventory.Instance != null && PlayerInventory.Instance.appleText != null)
        {
            PlayerInventory.Instance.appleText.gameObject.SetActive(false);
        }

        Debug.Log("¡Misión de manzanas completada!");
    }

    private void OnDestroy()
    {
        if (simpleInteractable != null)
            simpleInteractable.selectEntered.RemoveListener(OnNPCSelected);
    }
}
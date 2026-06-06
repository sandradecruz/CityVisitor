using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AppleInteractable : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable simpleInteractable;

    void Start()
    {
        simpleInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
  
        simpleInteractable.selectEntered.AddListener(OnPickedUp);
    }

    void OnPickedUp(SelectEnterEventArgs args)
    {
        if (PlayerInventory.Instance != null)
        {
            
            PlayerInventory.Instance.appleCount++;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
               
                Vector3 positionBehind = player.transform.position - (player.transform.forward * 0.4f) + (Vector3.up * 1.0f);
                
                transform.position = positionBehind;
                
                
                transform.SetParent(player.transform);
            }

            
            if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().isKinematic = true;
            if (GetComponent<Collider>()) GetComponent<Collider>().enabled = false;

           
            simpleInteractable.enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (simpleInteractable != null)
            simpleInteractable.selectEntered.RemoveListener(OnPickedUp);
    }
}
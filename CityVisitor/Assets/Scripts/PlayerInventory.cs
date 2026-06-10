using UnityEngine;
using TMPro; 

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int _appleCount = 0;
    
    public TextMeshProUGUI appleText; 

    public static PlayerInventory Instance;

    public int appleCount
    {
        get { return _appleCount; }
        set 
        { 
            _appleCount = value; 
            UpdateAppleUI(); 
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateAppleUI();
    }

    public void UpdateAppleUI()
    {
        if (appleText != null)
        {
            appleText.text = "Manzanas = " + _appleCount;

            //  Muestra el texto solo si hay 1 o más manzanas
            if (_appleCount > 0.5)
            {
                appleText.gameObject.SetActive(true);
            }
            else
            {
                appleText.gameObject.SetActive(false);
            }
        }
    }

    //Elimina las manzanas del jugador
    public void ClearCollectedApples()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            
            AppleInteractable[] physicalApples = player.GetComponentsInChildren<AppleInteractable>();

            foreach (AppleInteractable apple in physicalApples)
            {
                Destroy(apple.gameObject); 
            }
        }
    }
}
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
        }
    }
}
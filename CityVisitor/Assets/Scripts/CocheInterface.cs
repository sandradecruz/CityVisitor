using UnityEngine;
using UnityEngine.InputSystem; 

public class VRCarInterface : MonoBehaviour
{
    public SimpleCarController carController;
    
    [Header("Referencias VR")]
    public GameObject vrPlayerRig; 
    public MonoBehaviour vrMovementScript; 
    
    [Header("Anclajes")]
    public Transform seatAnchor;
    public Transform exitAnchor;

    [Header("Configuración de Controles VR (Action Based)")]
    public InputActionProperty rightTriggerAcceleration; // Gatillo Derecho -> Acelerar
    public InputActionProperty leftTriggerBrakeBackwards; // Gatillo Izquierdo -> Freno / Marcha Atrás
    public InputActionProperty leftJoystickSteering;     // Joystick Izquierdo -> Girar
    public InputActionProperty exitVehicleAction;         // Botón B Mando Derecho -> Bajar

    private bool isPlayerInside = false;
    private CharacterController cachedCharacterController;
    private Rigidbody carRigidbody;

    void Start()
    {
        // Guardamos el Rigidbody del coche para comprobar su velocidad local
        carRigidbody = carController.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rightTriggerAcceleration.action.Enable();
        leftTriggerBrakeBackwards.action.Enable();
        leftJoystickSteering.action.Enable();
        exitVehicleAction.action.Enable();
    }

    public void EnterVehicle()
    {
        if (isPlayerInside) return;

        isPlayerInside = true;
        carController.isDriving = true;

        if (vrMovementScript != null) vrMovementScript.enabled = false;

        cachedCharacterController = vrPlayerRig.GetComponentInChildren<CharacterController>();
        if (cachedCharacterController != null) cachedCharacterController.enabled = false;

        vrPlayerRig.transform.SetParent(transform);
        vrPlayerRig.transform.position = seatAnchor.position;
        vrPlayerRig.transform.rotation = seatAnchor.rotation;
    }

    public void ExitVehicle()
    {
        if (!isPlayerInside) return;

        isPlayerInside = false;
        carController.isDriving = false;

        vrPlayerRig.transform.SetParent(null);
        vrPlayerRig.transform.position = exitAnchor.position;
        vrPlayerRig.transform.rotation = exitAnchor.rotation;

        if (cachedCharacterController != null) cachedCharacterController.enabled = true;
        if (vrMovementScript != null) vrMovementScript.enabled = true;
    }

    void Update()
    {
        if (isPlayerInside)
        {
            // 1. DIRECCIÓN
            Vector2 steerInput = leftJoystickSteering.action.ReadValue<Vector2>();
            float steer = steerInput.x; 

            // 2. ACELERACIÓN Y MARCHA ATRÁS INTELIGENTE
            float throttleRight = rightTriggerAcceleration.action.ReadValue<float>(); 
            float brakeLeft = leftTriggerBrakeBackwards.action.ReadValue<float>();
            
            float throttle = 0f;
            bool brake = false;

            // Detectar si el coche va hacia adelante o hacia atrás localmente
            float localForwardSpeed = 0f;
            if (carRigidbody != null)
            {
                localForwardSpeed = transform.InverseTransformDirection(carRigidbody.linearVelocity).z;
            }

            // Lógica de pedales:
            if (brakeLeft > 0.1f)
            {
                // Si vamos hacia adelante a cierta velocidad, el gatillo izquierdo FRENA
                if (localForwardSpeed > 0.5f)
                {
                    brake = true;
                    throttle = 0f;
                }
                // Si el coche ya está casi parado o yendo marcha atrás, el gatillo izquierdo acelera hacia ATRÁS
                else
                {
                    brake = false;
                    throttle = -brakeLeft; // Valor negativo para que el motor empuje hacia atrás
                }
            }
            
            // Si no usamos el gatillo izquierdo, aceleramos normal hacia adelante con el derecho
            if (!brake && throttleRight > 0.1f)
            {
                throttle = throttleRight;
            }

            // 3. SOPORTE DE TESTEO PARA PC
            if (Application.isEditor && throttleRight == 0 && brakeLeft == 0 && steer == 0)
            {
                steer = Input.GetAxis("Horizontal");
                throttle = Input.GetAxis("Vertical");
                brake = Input.GetKey(KeyCode.Space);
            }

            // Enviar entradas procesadas al coche
            carController.SetInputs(throttle, steer, brake);

            // 4. DETECTAR BOTÓN PARA BAJARSE (Mando VR o Tecla E en PC)
            if (exitVehicleAction.action.WasPressedThisFrame() || Input.GetKeyDown(KeyCode.E))
            {
                ExitVehicle();
            }
        }
    }
}
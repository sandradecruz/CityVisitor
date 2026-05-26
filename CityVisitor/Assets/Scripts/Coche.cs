using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    [Header("Wheel Colliders Invisibles")]
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;

    [Header("Ruedas Visuales (Tus sub-objetos)")]
    public Transform frontLeftVisual;
    public Transform frontRightVisual;
    public Transform rearLeftVisual;
    public Transform rearRightVisual;

    [Header("Ajustes del Coche")]
    public float maxMotorTorque = 1000f; // Fuerza de aceleración
    public float maxSteeringAngle = 30f; // Giro de las ruedas delanteras
    public float brakeTorque = 3000f;    // Fuerza de frenado

    [HideInInspector] public bool isDriving = false;

    private float motorInput;
    private float steeringInput;
    private bool isBraking;

    // Método para recibir los datos desde el script de VR
    public void SetInputs(float throttle, float steer, bool brake)
    {
        motorInput = throttle;
        steeringInput = steer;
        isBraking = brake;
    }
void Start()
{
    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null)
    {
        // Baja el centro de masas 1.5 metros por debajo del origen del coche
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }
}
    void FixedUpdate()
    {
        if (!isDriving)
        {
            ApplyBrake(brakeTorque); // Frenar automáticamente si no hay nadie dentro
            return;
        }

        // 1. Girar ruedas delanteras
        float steeringAngle = steeringInput * maxSteeringAngle;
        frontLeftCollider.steerAngle = steeringAngle;
        frontRightCollider.steerAngle = steeringAngle;

        // 2. Acelerar / Frenar (Tracción trasera en este ejemplo)
        if (isBraking)
        {
            ApplyBrake(brakeTorque);
        }
        else
        {
            ApplyBrake(0);
            rearLeftCollider.motorTorque = motorInput * maxMotorTorque;
            rearRightCollider.motorTorque = motorInput * maxMotorTorque;
        }

        // 3. Actualizar la posición y rotación visual de las ruedas
        UpdateWheelVisual(frontLeftCollider, frontLeftVisual);
        UpdateWheelVisual(frontRightCollider, frontRightVisual);
        UpdateWheelVisual(rearLeftCollider, rearLeftVisual);
        UpdateWheelVisual(rearRightCollider, rearRightVisual);
    }

    void ApplyBrake(float amount)
    {
        frontLeftCollider.brakeTorque = amount;
        frontRightCollider.brakeTorque = amount;
        rearLeftCollider.brakeTorque = amount;
        rearRightCollider.brakeTorque = amount;
        if (amount > 0)
        {
            rearLeftCollider.motorTorque = 0;
            rearRightCollider.motorTorque = 0;
        }
    }

    void UpdateWheelVisual(WheelCollider collider, Transform visualTransform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        visualTransform.position = pos;
        visualTransform.rotation = rot;
    }
}
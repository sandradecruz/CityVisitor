using UnityEngine;

using UnityEngine;

public class ControlCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvasObjeto;

    // Esta función la llamaremos desde el evento de Unity
    public void AlternarEstado()
    {
        if (canvasObjeto != null)
        {
            // Cambia al estado opuesto del actual (si es true pasa a false y viceversa)
            bool estadoActual = canvasObjeto.activeSelf;
            canvasObjeto.SetActive(!estadoActual);
        }
    }
}

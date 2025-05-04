using UnityEngine;

/*
 * Activa el movimiento de la puerta
 * Luego hay que comunicar con inventario y modificar el script para que la puerta tenga atributo de locked
 * Y según id del item o algo se pueda abrir si se tiene en el inventario
 */

public class ObjectInteractDoor : MonoBehaviour
{
    public bool doorOpen = false;
    public float doorOpenAngle = 95f;       // Ángulo al abrir
    public float doorCloseAngle = 0.0f;     // Ángulo al cerrar
    public float smooth = 3.0f;             // Velocidad de movimiento de la puerta

    public void ChangeDoorState()
    {
        doorOpen = !doorOpen;
    }

    // 👇 ESTE ES EL NUEVO MÉTODO QUE AGREGA LA FUNCIONALIDAD PARA EL KEYPAD
    public void OpenFromKeypad()
    {
        doorOpen = true;
    }

    public void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(0, doorOpen ? doorOpenAngle : doorCloseAngle, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}

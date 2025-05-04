using UnityEngine;

public class CameraSwing : MonoBehaviour
{
    [Header("Balanceo de c�mara")]
    public float verticalAmplitude = 0.05f; // Amplitud del balanceo vertical
    public float verticalFrequency = 2.0f; // Frecuencia del balanceo vertical
    public float horizontalAmplitude = 0.05f; // Amplitud del balanceo horizontal
    public float horizontalFrequency = 1.5f; // Frecuencia del balanceo horizontal

    private Vector3 initialPosition;
    private float verticalTimer;
    private float horizontalTimer;

    void Start()
    {
        // Guardamos la posici�n inicial de la c�mara
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Actualizamos los temporizadores
        verticalTimer += Time.deltaTime * verticalFrequency;
        horizontalTimer += Time.deltaTime * horizontalFrequency;

        // Calculamos el balanceo en los ejes X (horizontal) e Y (vertical) usando funciones seno
        float verticalOffset = Mathf.Sin(verticalTimer) * verticalAmplitude;
        float horizontalOffset = Mathf.Sin(horizontalTimer) * horizontalAmplitude;

        // Actualizamos la posici�n de la c�mara con ambos offsets
        transform.localPosition = initialPosition + new Vector3(horizontalOffset, verticalOffset, 0);
    }
}
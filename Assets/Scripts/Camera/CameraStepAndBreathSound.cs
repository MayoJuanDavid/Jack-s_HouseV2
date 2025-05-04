using UnityEngine;

public class CameraStepAndBreathSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip stepSound;
    public AudioClip breathingSound;
    public int stepsBetweenSounds = 2; // N�mero de pasos necesarios antes de reproducir el sonido de pasos
    public float breathingInterval = 5.0f; // Tiempo en segundos entre cada sonido de respiraci�n

    private int stepCounter = 0; // Contador de pasos
    private float breathingTimer = 0; // Temporizador para el sonido de respiraci�n

    void Start()
    {
        // Configuraci�n inicial del AudioSource
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Control del sonido de pasos
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            // Incrementamos el contador de pasos
            stepCounter++;

            if (stepCounter >= stepsBetweenSounds)
            {
                audioSource.clip = stepSound;
                audioSource.Play();

                // Reiniciamos el contador de pasos
                stepCounter = 0;
            }
        }

        // Control del sonido de respiraci�n
        breathingTimer += Time.deltaTime; // Incrementamos el temporizador

        if (breathingTimer >= breathingInterval)
        {
            audioSource.clip = breathingSound;
            audioSource.Play();

            // Reiniciamos el temporizador de respiraci�n
            breathingTimer = 0;
        }
    }
}
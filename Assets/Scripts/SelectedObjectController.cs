using UnityEngine;

public class SelectedObjectController : MonoBehaviour
{
    public LayerMask objectMask;
    public float distance = 1.5f;
    public Texture2D puntero;
    public GameObject textDetect;

    public KeypadUIPopup keypadUI;              // ← NUEVO: asignas en el Inspector
    public string keypadTag = "Keypad";         // ← Opcional: puedes cambiar la tag

    private GameObject ultimoReconocido = null;

    void Start()
    {
        textDetect.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, objectMask))
        {
            Deselect();
            SelectObject(hit.transform);

            // Si el objeto detectado es el Keypad y presionan E → abre UI
            if (hit.collider.CompareTag(keypadTag) && Input.GetKeyDown(KeyCode.E))
            {
                keypadUI.OpenPanel();
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.red);
        }
        else
        {
            Deselect();
        }
    }

    void SelectObject(Transform transform)
    {
        var renderer = transform.GetComponent<Renderer>();
        if (renderer)
        {
            renderer.material.color = Color.cyan;
        }
        ultimoReconocido = transform.gameObject;
    }

    void Deselect()
    {
        if (ultimoReconocido)
        {
            var renderer = ultimoReconocido.GetComponent<Renderer>();
            if (renderer)
            {
                renderer.material.color = Color.white;
            }
            ultimoReconocido = null;
        }
    }

    void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
        GUI.DrawTexture(rect, puntero);

        textDetect.SetActive(ultimoReconocido != null);
    }
}

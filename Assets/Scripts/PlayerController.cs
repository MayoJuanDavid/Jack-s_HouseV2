using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;     //Para que sea primera persona
    public float walkSpeed = 3f;    //Velocidad de movimientos
    public float runSpeed = 6f;
    public float jumpPower = 4f;
    public float gravity = 10f;
    public float lookSpeed = 2f;    //Sensibilidad
    public float lookXLimit = 45f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        InteraccionObjetos();
    }

    /*
     * Aqui se debe programar todo lo que pasara cuando el personaje haga realice una accion con un objeto
     * Controlando las acciones por los Tags
     */
    void InteraccionObjetos()
    {
        RaycastHit hit; // Declarar el RaycastHit
        float distance = 1.5f;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Raycast para comprobar si hay un objeto delante, accion dependiendo del Tag
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                if (hit.collider.CompareTag("ObjectInteractDoor"))
                {
                    hit.collider.GetComponent<ObjectInteractDoor>()?.ChangeDoorState();
                }
                else if (hit.collider.CompareTag("Collectable")) // Modular cambiando el TAG, Para Inventory
                {
                    // hit.collider.GetComponent<SCRIPT>()?.FUNCTION();
                }
            }
        }
    }

    //Control basico del Personaje
    void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
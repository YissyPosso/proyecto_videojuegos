using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float jumpForce = 5f;

    public Transform cameraTransform;   // Arrastra aquí la cámara en el inspector
    private float xRotation = 0f;        // Acumula rotación vertical

    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        // Movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0.0f, vertical) * Time.deltaTime * speed);

        // Rotación horizontal del jugador (gira el cuerpo)
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, mouseX, 0f);

        // Rotación vertical de la cámara (mirar arriba y abajo)
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        xRotation -= mouseY;                     // invertimos porque en Unity es al revés
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // limita cuánto puedes mirar

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

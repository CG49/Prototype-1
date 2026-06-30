using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement tuning ( editable in Inspector
    public float speed = 10f;
    public float turnSpeed = 50f;
    // Input System action exposed in Inspector for binding ( WASD/Arrow keys )
    private PlayerInput playerInput;
    public string playerID;
    public InputAction moveAction;

    // Current input value (x=left/right, y=forward/back), kept private for internal use
    private Vector2 moveInput;
    private Rigidbody rb;

    public Camera mainCamera;
    public Camera hoodCamera;
    public InputAction switchCameraAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerInput = GetComponent<PlayerInput>();

        if (playerID == "P1")
            moveAction = playerInput.actions["MoveP1"];
        else if (playerID == "P2")
            moveAction = playerInput.actions["MoveP2"];
        else
            moveAction = playerInput.actions["Move"];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //if (playerInput.playerIndex == 0)
        //    transform.position = new Vector3(-5, 0, 0);
        //else
        //    transform.position = new Vector3(5, 0, 0);
    }

    void OnEnable()
    {
        if (moveAction != null)
            moveAction.Enable();

        if (switchCameraAction != null)
            switchCameraAction.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null)
            moveAction.Disable();

        if (switchCameraAction != null)
            switchCameraAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Read the 2D vector from the moveAction (x: horizontal, y: vertical)
        moveInput = moveAction.ReadValue<Vector2>();

        if (switchCameraAction.WasPressedThisFrame())
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }

        // Move forward/back along local Z using the y component
        // transform.Translate( moveInput.y * speed * Time.deltaTime * Vector3.forward );

        // Rotate around local Y (y-axis) using the x component
        // transform.Rotate( Vector3.up, Time.deltaTime * turnSpeed * moveInput.x );
    }

    void FixedUpdate()
    {
        // Move forward/backward
        rb.MovePosition(
            rb.position +
            transform.forward * moveInput.y * speed * Time.fixedDeltaTime
        );

        // Rotate left/right
        Quaternion turn =
            Quaternion.Euler(
                0,
                moveInput.x * turnSpeed * Time.fixedDeltaTime,
                0);

        rb.MoveRotation(rb.rotation * turn);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Obstacle"))
            return;

        Rigidbody otherRb = collision.rigidbody;

        if (otherRb == null)
            return;

        Vector3 hitPoint = collision.contacts[0].point;

        Vector3 direction = (collision.transform.position - transform.position).normalized;

        direction.y = 0.5f;

        otherRb.AddForceAtPosition(direction * 20f, hitPoint, ForceMode.Impulse);
    }
}

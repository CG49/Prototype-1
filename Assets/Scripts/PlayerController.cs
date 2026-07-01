using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;

    // Input System action exposed in Inspector for binding ( WASD/Arrow keys )
    private PlayerInput playerInput;
    public string playerID;
    public InputAction moveAction;

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

    void OnEnable()
    {
        moveAction?.Enable();

        switchCameraAction?.Enable();
    }

    void OnDisable()
    {
        moveAction?.Disable();

        switchCameraAction?.Disable();
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
    }

    void FixedUpdate()
    {
        // Move forward/backward
        rb.MovePosition(
            rb.position +
            (moveInput.y * speed * Time.fixedDeltaTime * transform.forward)
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

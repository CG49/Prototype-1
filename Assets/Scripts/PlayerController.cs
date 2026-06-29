using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement tuning ( editable in Inspector
    public float speed = 10f;
    public float turnSpeed = 50f;

    // Input System action exposed in Inspector for binding ( WASD/Arrow keys )
    public InputAction MoveAction;
    // Current input value (x=left/right, y=forward/back), kept private for internal use
    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        // Enable the MoveAction so it starts reading input
        if (MoveAction != null)
            MoveAction.Enable();
    }

    void OnDisable()
    {
        if (MoveAction != null)
            MoveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Read the 2D vector from the MoveAction (x: horizontal, y: vertical)
        moveInput = MoveAction.ReadValue<Vector2>();

        // Move forward/back along local Z using the y component
        transform.Translate( moveInput.y * speed * Time.deltaTime * Vector3.forward );

        // Rotate around local Y (y-axis) using the x component
        transform.Rotate( Vector3.up, Time.deltaTime * turnSpeed * moveInput.x );
    }
}

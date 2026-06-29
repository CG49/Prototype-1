using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerX : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 80f;

    public InputAction verticalAction;

    void OnEnable()
    {
        if (verticalAction != null)
            verticalAction.Enable();
    }

    void OnDisable()
    {
        if (verticalAction != null)
            verticalAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // get the user's vertical input
        float verticalInput = verticalAction.ReadValue<float>();

        // move the plane forward at a constant rate
        transform.Translate(speed * Time.deltaTime * Vector3.forward);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(rotationSpeed * Time.deltaTime * verticalInput * Vector3.left);
    }
}

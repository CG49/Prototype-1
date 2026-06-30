using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Movement tuning ( editable in Inspector
    public float speed = 15f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(
            rb.position +
            transform.forward * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Rigidbody rb = collision.rigidbody;

        //if (rb != null)
        //{
        //    Vector3 hitPoint = collision.contacts[0].point;

        //    Vector3 direction = (collision.transform.position - transform.position).normalized;
        //    direction.y = 0.5f;

        //    rb.AddForceAtPosition(direction * 20f, hitPoint, ForceMode.Impulse);
        //}
    }
}

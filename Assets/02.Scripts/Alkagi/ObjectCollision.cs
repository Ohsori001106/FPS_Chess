using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public Rigidbody rb;
    public float bounceForce = 2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Collision detection to continuous
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Enable interpolation for smoother movement
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 bounceDirection = collision.contacts[0].normal;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}

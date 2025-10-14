using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;        // WASD movement
    public float jumpForce = 5f;        // Space jump
    public float maxSpeed = 10f;        // Clamp velocity

    Rigidbody rb;
    Vector3 input;

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update() // Input & non-physics
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        input = new Vector3(h, 0f, v).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.05f)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void FixedUpdate() // Physics
    {
        Vector3 targetVel = input * moveSpeed;
        Vector3 vel = rb.velocity;
        Vector3 velChange = (targetVel - new Vector3(vel.x, 0f, vel.z));

        rb.AddForce(velChange, ForceMode.VelocityChange);

        Vector3 horiz = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (horiz.magnitude > maxSpeed)
        {
            Vector3 clamped = horiz.normalized * maxSpeed;
            rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);
        }
    }
}
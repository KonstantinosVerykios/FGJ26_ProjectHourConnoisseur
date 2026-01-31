using UnityEngine;
using UnityEngine.InputSystem;

sing UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardForce = 50f;
    public float maxSpeed = 10f;

    [Header("Steering")]
    public float turnTorque = 10f;

    Rigidbody rb;
    float steerInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Always move forward
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * forwardForce, ForceMode.Force);
        }

        // Steering
        rb.AddTorque(Vector3.up * steerInput * turnTorque, ForceMode.Force);
    }

    // Called by Input System
    public void OnSteer(InputAction.CallbackContext context)
    {
        steerInput = context.ReadValue<float>();
    }
}
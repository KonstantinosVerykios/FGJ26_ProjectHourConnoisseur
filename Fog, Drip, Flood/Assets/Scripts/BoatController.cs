using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float MovingSpeed = 100f;
    [SerializeField] private float TurningSpeed = 100f;

    [Header("Events")]
    public UnityEvent<bool> PaddleStart;    // left = true, right = false
    public UnityEvent<bool> PaddleStop;     // left = true, right = false

    [Header("TrashCodeTM")]
    bool _leftWasPressed;
    bool _rightWasPressed;

    Rigidbody rb;

    Vector2 move;

    public Player inputActions;

    private InputAction steer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new Player();

        steer = inputActions.BoatInputActions.Movement;
    }

    private void OnEnable()
    {
        steer.Enable();
        steer.canceled += OnMoveCancel;
        steer.performed += OnMove;
    }

    private void OnDisable()
    {
        steer.Disable();
        steer.canceled -= OnMoveCancel;
        steer.performed -= OnMove;
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        move = value.ReadValue<Vector2>();

        // send signal about what going on
        _leftWasPressed = move.x < 0;
        _rightWasPressed = move.x > 0;
        PaddleStart.Invoke(_leftWasPressed);
    }

    public void OnMoveCancel(InputAction.CallbackContext value)
    {
        move = Vector2.zero;

        // send signal about what going on
        if (_leftWasPressed && !Keyboard.current.aKey.isPressed)
        {
            _leftWasPressed = false;
            print("left released");
            PaddleStop.Invoke(true);
        }
        else if (_rightWasPressed && !Keyboard.current.dKey.isPressed)
        {
            _rightWasPressed = false;
            print("right released");
            PaddleStop.Invoke(false);
        }
    }
    private void FixedUpdate()
    {
        // Check if boat.z vector and world z.vector dot product is  

        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 forwardForce = transform.forward * MovingSpeed;

        rb.AddForce(forwardForce, ForceMode.Acceleration);
        rb.AddTorque(Vector3.up * move.x * TurningSpeed, ForceMode.Acceleration);
    }

}
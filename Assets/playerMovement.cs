using UnityEngine;
using UnityEngine.InputSystem;
// sry i had to update script to get it to work with interaction system

public class playerMovement : MonoBehaviour
{
    public float speed;
    public float dSpeed;

    public InputAction movementControls;
    public InputAction actions;

    private Rigidbody2D rb;
    private Camera mainCamera;

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 mouseWorldPos = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        movementControls.Enable();
        actions.Enable();
    }

    private void OnDisable()
    {
        movementControls.Disable();
        actions.Disable();
    }

    void Update()
    {
        moveDirection = movementControls.ReadValue<Vector2>();

        Vector2 screenPos = Mouse.current.position.ReadValue();
        mouseWorldPos = mainCamera.ScreenToWorldPoint(screenPos);
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);

        Vector2 direction = (mouseWorldPos - rb.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);
    }
    private void Dash()
    {
        //player.linearVelocity = 
    }
}
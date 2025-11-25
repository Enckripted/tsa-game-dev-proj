using UnityEngine;
using System.Collections.Generic;
// had to redo player movement again lmao 
// this time it actuall has collision detection
public class PlayerMovement8Dir : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float collisionOffset = 0.05f;

    [SerializeField] private ContactFilter2D movementFilter;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private InputSystem_Actions controls;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new InputSystem_Actions();
    }

    private void Start()
    {
        movementFilter.useTriggers = false;
        movementFilter.useLayerMask = true;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Update()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (moveInput == Vector2.zero) return;

        if (!TryMove(moveInput))
        {
            if (!TryMove(new Vector2(moveInput.x, 0)))
            {
                TryMove(new Vector2(0, moveInput.y));
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero) return false;

        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }
}
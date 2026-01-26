using UnityEngine;
using System.Collections.Generic;

public class Player4Dir : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 50f;
    [SerializeField] private float collisionOffset = 0.12f;
    [SerializeField] private ContactFilter2D movementFilter;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private InputSystem_Actions controls;
    private readonly List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Animator animator;
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private void Awake()
    {
        // best practice apparently
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controls = new InputSystem_Actions();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
    }

    // standard unity stuff
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
        if (GameState.GamePaused) return;

        moveInput = controls.Player.Move.ReadValue<Vector2>();
        animator.SetFloat(Horizontal, moveInput.x);
        animator.SetFloat(Vertical, moveInput.y);
    }

    private void FixedUpdate()
    {
        // movement vect doesnt go above 1 so we mult with max speed
        Vector2 targetVelocity = moveInput * maxSpeed;

        // safety checks
        if (moveInput.magnitude > 0.01f)
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime); // accelerate
            //HandleRotation(moveInput);
        }
        else
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime); // decelerate
        }

        if (currentVelocity.magnitude > 0.001f)
        {
            Move(currentVelocity * Time.fixedDeltaTime); // actually move
        }
    }
    /*
    private void HandleRotation(Vector2 direction)
    {
        // idk i pulled this from a yt tutorial
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
    }
    */
    private void Move(Vector2 moveAmount)
    {
        float distance = moveAmount.magnitude;

        // returns number of collisions
        int count = rb.Cast(
            moveAmount,
            movementFilter,
            castCollisions,
            distance + collisionOffset
        );

        // if no collision move normally
        if (count == 0)
        {
            rb.MovePosition(rb.position + moveAmount);
            return;
        }

        // sliding logic, also from yt tutorial
        Vector2 hitNormal = castCollisions[0].normal; // get first collision normal

        Vector2 slideDirection = Vector2.Perpendicular(hitNormal).normalized; // normalize and perpendicular

        float dot = Vector2.Dot(moveAmount, slideDirection); // project move amount onto slide direction
        Vector2 slideAmount = slideDirection * dot;

        // check for collisions in slide direction
        int slideCount = rb.Cast(
            slideAmount,
            movementFilter,
            castCollisions,
            slideAmount.magnitude + collisionOffset
        );

        // if no collisions then move
        if (slideCount == 0)
        {
            rb.MovePosition(rb.position + slideAmount);
        }
        else
        {
            // dont move, necessary to stop jittering when stuck
        }
    }
}

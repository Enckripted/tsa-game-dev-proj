using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
// works with colliisons by using a raycast instead of OnCollisionEnter2D

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 1f;
    [SerializeField] private LayerMask interactLayer;

    private Vector2 facingDirection = Vector2.down;


    public void OnInteract(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, interactRange, interactLayer);

        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && hit.collider.CompareTag("Interactable"))
            {
                interactable.Interact();
            }
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if (input != Vector2.zero)
        {
            facingDirection = input.normalized;
        }
    }
}
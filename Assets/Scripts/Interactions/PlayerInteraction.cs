using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 1.5f;
    [SerializeField] private float interactRadius = 0.5f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private Vector2 holdOffset = new Vector2(0, 1.5f);
    [SerializeField] private float requiredHoldTime = 0.4f;
    [SerializeField] private InteractionPromptUI promptPrefab;
    private InteractionPromptUI currentPrompt;
    private Interactable currentInteractable;
    private InputSystem_Actions controls;
    private Vector2 facingDirection = Vector2.down;
    private float currentHoldTimer = 0f;
    private bool isHolding = false;

    // standard unity stuff
    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Start()
    {
        // clone prefab and hide it
        currentPrompt = Instantiate(promptPrefab);
        currentPrompt.Close();
    }

    private void Update()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        if (moveInput.sqrMagnitude > 0.1f) // sqr magnitude for better performance 🤓
        {
            facingDirection = moveInput.normalized;
        }

        bool inputActive = controls.Player.Interact.ReadValue<float>() > 0.1f;

        DetectInteractable();
        HandleInteractionLogic(inputActive);
    }

    private void DetectInteractable()
    {
        // CircleCast instead of Raycast = better detection
        RaycastHit2D hit = Physics2D.CircleCast(
            transform.position,
            interactRadius,
            facingDirection,
            interactRange,
            interactLayer
        );

        if (hit.collider != null)
        {
            if (!hit.collider.CompareTag("Interactable")) // check tags
            {
                if (currentInteractable != null)
                {
                    ClearInteraction();
                }
                return;
            }

            Interactable newInteractable = hit.collider.GetComponent<Interactable>(); // get interactable component once detected

            if (newInteractable != null)
            {
                if (currentInteractable != newInteractable)
                {
                    currentInteractable = newInteractable;

                    currentPrompt.transform.position = (Vector2)hit.collider.transform.position + holdOffset; // position prompt above interactable
                    currentPrompt.Setup("[ Hold E ]"); // setup prompt text
                }
            }
            else // bunch of checks to reset vars
            {
                if (currentInteractable != null)
                {
                    ClearInteraction();
                }
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                ClearInteraction();
            }
        }
    }

    private void HandleInteractionLogic(bool isButtonPressed)
    {
        // safety checks n stuff
        if (currentInteractable == null)
        {
            return;
        }

        if (isButtonPressed)
        {
            if (!isHolding)
            {
                isHolding = true;
                currentHoldTimer = 0f;
            }

            currentHoldTimer += Time.deltaTime;

            float progress = Mathf.Clamp01(currentHoldTimer / requiredHoldTime); // calculate progress maxed at 1
            currentPrompt.UpdateProgress(progress); // update that bih

            if (currentHoldTimer >= requiredHoldTime) // checks if time is good
            {
                PerformInteraction();
            }
        }
        else
        {
            if (isHolding) // reset vars
            {
                isHolding = false;
                currentHoldTimer = 0f;
                currentPrompt.UpdateProgress(0f);
            }
        }
    }

    // helper methods 
    private void PerformInteraction()
    {
        currentInteractable.Interact();

        isHolding = false;
        currentHoldTimer = 0f;
        currentPrompt.UpdateProgress(0f);

        currentPrompt.Close();
    }

    private void ClearInteraction()
    {
        currentInteractable = null;
        isHolding = false;
        currentHoldTimer = 0f;
        currentPrompt.Close();
    }
}
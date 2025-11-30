using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRadius = 0.5f;
    [SerializeField] private Vector2 holdOffset = new Vector2(0, 1.5f);
    [SerializeField] private float requiredHoldTime = 0.4f;
    [SerializeField] private InteractionPromptUI promptPrefab;
    [SerializeField] private ContactFilter2D interactableFilter;
    private InteractionPromptUI currentPrompt;
    private Interactable currentInteractable;
    private InputSystem_Actions controls;
    private Collider2D playerCollider;
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
        playerCollider = gameObject.GetComponent<Collider2D>();
        // clone prefab and hide it
        currentPrompt = Instantiate(promptPrefab);
        currentPrompt.Close();
    }

    private void Update()
    {
        bool inputActive = controls.Player.Interact.ReadValue<float>() > 0.1f;

        DetectInteractable();
        HandleInteractionLogic(inputActive);
    }

    private void DetectInteractable()
    {
        //changed this to be a circle around the player so i don't have to fiddle with the direction -diego
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, interactRadius, interactableFilter, results);

        Collider2D hitCollider = null;
        foreach (Collider2D collider in results)
        {
            if (!collider.CompareTag("Interactable")) continue;
            if (hitCollider == null || collider.Distance(playerCollider).distance < hitCollider.Distance(playerCollider).distance)
                hitCollider = collider;
        }

        //nothing detected, run the clear interaction routine
        if (hitCollider == null)
        {
            ClearInteraction();
            return;
        }

        Interactable newInteractable = hitCollider.GetComponent<Interactable>(); // get interactable component once detected
        if (newInteractable != null)
        {
            if (currentInteractable != newInteractable)
            {
                currentInteractable = newInteractable;
                currentPrompt.transform.position = (Vector2)hitCollider.transform.position + holdOffset; // position prompt above interactable
                currentPrompt.Setup(currentInteractable.interactText); // setup prompt text
                requiredHoldTime = currentInteractable.interactTime;
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

    private void HandleInteractionLogic(bool isButtonPressed)
    {
        // safety checks n stuff
        if (currentInteractable == null)
        {
            return;
        }

        if (controls.Player.Interact.WasPressedThisFrame()) isHolding = true;

        if (isButtonPressed)
        {
            /*
            Debug.Log(controls.Player.Interact.WasPressedThisFrame());
            if (controls.Player.Interact.WasPressedThisFrame())
            {
                isHolding = true;
                currentHoldTimer = 0f;
            } 
            */

            if (isHolding) currentHoldTimer += Time.deltaTime;

            float progress = Mathf.Clamp01(currentHoldTimer / requiredHoldTime); // calculate progress maxed at 1
            currentPrompt.UpdateProgress(progress); // update that bih

            if (currentHoldTimer >= requiredHoldTime) // checks if time is good
            {
                PerformInteraction();
            }
        }
        else
        {
            isHolding = false;
            currentHoldTimer = 0f;
            currentPrompt.UpdateProgress(0f);
        }
    }

    // helper methods 
    private void PerformInteraction()
    {
        currentInteractable.Interact();

        isHolding = false;
        currentHoldTimer = 0f;
        currentPrompt.UpdateProgress(0f);

        //commenting this because it feels a bit weird to open a machine ui, close it, and then not be able to open it again -diego
        //also if the object is gone then next frame the script will realize and remove the prompt anyways
        //currentPrompt.Close();
    }

    private void ClearInteraction()
    {
        currentInteractable = null;
        isHolding = false;
        currentHoldTimer = 0f;
        currentPrompt.Close();
    }
}
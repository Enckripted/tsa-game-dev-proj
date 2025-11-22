using UnityEngine;
// tbh i don't know how ts gonna work with real collision but wtv, thats for future me 
public class PlayerInteraction : MonoBehaviour
{
    private Interactable currentInteractable;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();

        if (interactable != null)
        {

            currentInteractable = interactable;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>() == currentInteractable)
        {

            currentInteractable = null;
        }
    }

    public void OnInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}
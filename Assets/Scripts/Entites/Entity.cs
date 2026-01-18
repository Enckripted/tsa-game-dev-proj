using UnityEngine;

/*
This a base class that just specifies an interactable and an hook on the interactable.
*/
[RequireComponent(typeof(Interactable))]
public abstract class Entity : MonoBehaviour
{
    protected Interactable InteractionPrompt;

    protected abstract void OnInteract();
    protected abstract void OnStart();

    void Start()
    {
        InteractionPrompt = GetComponent<Interactable>();
        InteractionPrompt.InteractEvent.AddListener(OnInteract);
        OnStart();
    }
}

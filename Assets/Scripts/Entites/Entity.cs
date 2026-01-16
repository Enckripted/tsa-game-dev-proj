using UnityEngine;

[RequireComponent(typeof(Interactable))]
public abstract class Entity : MonoBehaviour
{
    protected Interactable InteractionPrompt;

    protected abstract void OnInteract();
    protected abstract void OnStart();

    void Awake()
    {
        InteractionPrompt = GetComponent<Interactable>();
    }

    void Start()
    {
        InteractionPrompt.InteractEvent.AddListener(OnInteract);
        OnStart();
    }
}

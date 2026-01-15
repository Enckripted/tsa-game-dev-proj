using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [field: SerializeField] public string InteractText { get; private set; } = "[E] Interact";
    public UnityEvent InteractEvent { get; private set; }

    public void Interact()
    {
        InteractEvent.Invoke();
    }

    void Awake()
    {
        InteractEvent = new UnityEvent();
    }
}

using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [field: SerializeField] public string interactText { get; private set; } = "[E] Interact";
    [field: SerializeField] public float interactTime { get; private set; } = 0.4f;
    public UnityEvent interactEvent { get; private set; }

    public void Interact()
    {
        interactEvent.Invoke();
    }

    void Awake()
    {
        interactEvent = new UnityEvent();
    }
}

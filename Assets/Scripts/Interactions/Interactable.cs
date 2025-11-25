using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        Destroy(gameObject); // placeholder script add Interact functionality to other scripts
    }
}

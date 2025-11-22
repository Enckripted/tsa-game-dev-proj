using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        Destroy(gameObject); // replace ts with adding to invetory or sum
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUiSlot : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Outline outline;

    public Item itemInSlot;
    public bool selected;

    /*
    void onChange()
    {
        if (inventorySlot.containsItem)
        {
            nameText.text = inventorySlot.item.name;
        }
        else
        {
            nameText.text = "";
        }
    }
    */

    void Update()
    {
        outline.enabled = selected;

        if (itemInSlot == null)
        {
            nameText.text = "";
            return;
        }
        nameText.text = itemInSlot.name;
    }
}

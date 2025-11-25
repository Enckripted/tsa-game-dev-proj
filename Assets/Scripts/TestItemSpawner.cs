using System.Collections.Generic;
using UnityEngine;

public class TestItemSpanwer : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    private DroppedItem droppedItem;

    void Update()
    {
        if (droppedItem == null)
        {
            GearItem chosenGear = RandItemGen.instance.genRandomGear();

            //we need to set data before the awake method is called
            GameObject obj = Instantiate(droppedItemPrefab, transform);
            droppedItem = obj.GetComponent<DroppedItem>();
            droppedItem.item = chosenGear;
            obj.SetActive(true);
        }
    }
}

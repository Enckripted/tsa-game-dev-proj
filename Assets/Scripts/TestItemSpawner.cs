using System.Collections.Generic;
using UnityEngine;

public class TestItemSpawner : MonoBehaviour
{
    public GameObject DroppedItemPrefab;

    private DroppedItem droppedItem;

    void Update()
    {
        if (droppedItem == null)
        {
            WandItem chosenWand = RandomItemFactory.CreateRandomWand();

            //we need to set data before the awake method is called
            GameObject obj = Instantiate(DroppedItemPrefab, transform);
            droppedItem = obj.GetComponent<DroppedItem>();
            droppedItem.Item = chosenWand;
            obj.SetActive(true);
        }
    }
}

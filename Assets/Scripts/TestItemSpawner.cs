using System.Collections.Generic;
using UnityEngine;

public class TestItemSpanwer : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    private DroppedItem droppedItem;

    private List<GearData> baseGears;
    private List<MaterialData> materials;

    T chooseFrom<T>(List<T> l)
    {
        return l[Random.Range(0, l.Count)];
    }

    void Start()
    {
        baseGears = new List<GearData>(Resources.LoadAll<GearData>("Base Gears"));
        materials = new List<MaterialData>(Resources.LoadAll<MaterialData>("Materials"));
    }

    void Update()
    {
        if (droppedItem == null)
        {
            GearData gearData = chooseFrom(baseGears);
            MaterialData materialData = chooseFrom(materials);
            Gear chosenGear = new Gear(gearData, new Material(materialData));

            //we need to set data before the awake method is called
            GameObject obj = Instantiate(droppedItemPrefab, transform);
            droppedItem = obj.GetComponent<DroppedItem>();
            droppedItem.item = new GearItem(chosenGear);
            obj.SetActive(true);
        }
    }
}

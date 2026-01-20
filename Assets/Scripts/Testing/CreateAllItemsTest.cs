using UnityEngine;

public class CreateAllItemsTest : MonoBehaviour
{
    public float ItemSpacing;
    public GameObject DroppedItemPrefab;

    private Vector3 currentPos;

    private void PlaceDroppedItem(IItem item)
    {
        GameObject nDroppedPrefab = Instantiate(DroppedItemPrefab, transform);
        nDroppedPrefab.transform.position = transform.position + currentPos;
        nDroppedPrefab.GetComponent<DroppedItem>().Item = item;
        nDroppedPrefab.SetActive(true);
    }

    private void CreateAllWandsOfType(WandScriptableObject wandData)
    {
        foreach (MaterialScriptableObject materialData in ScriptableObjectData.BaseMaterials)
        {
            PlaceDroppedItem(new WandItem(wandData, 1, new Material(materialData), 0));
            currentPos += Vector3.right * ItemSpacing;
        }
        currentPos.x = 0;
    }

    private void CreateAllWands()
    {
        foreach (WandScriptableObject wandData in ScriptableObjectData.BaseWands)
        {
            CreateAllWandsOfType(wandData);
            currentPos += Vector3.down * ItemSpacing;
        }
    }

    private void CreateAllGems()
    {
        foreach (GemScriptableObject gemData in ScriptableObjectData.BaseGems)
        {
            PlaceDroppedItem(new GemItem(gemData));
            currentPos += new Vector3(ItemSpacing, 0);
        }
        currentPos.x = 0;
        currentPos += Vector3.down * ItemSpacing;
    }

    void Start()
    {
        CreateAllWands();
        CreateAllGems();
        Debug.Log("Finished Create All Items");
        Debug.Log("Final posiition is " + currentPos);
    }
}

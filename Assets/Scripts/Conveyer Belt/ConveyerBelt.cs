using UnityEngine;
using System.Collections.Generic;

public class ConveyerBelt : MonoBehaviour
{
    // vars to change belt
    [SerializeField] private float beltSpeed = 5f;
    [SerializeField] private Vector2 beltDirection = Vector2.down;
    [SerializeField] private float beltLength = 10f;

    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int maxItemsOnBelt = 10;
    [SerializeField] private Vector2 spawnOffset = Vector2.zero;
    [SerializeField] private float minItemSpacing = 1.5f;

    private List<BeltItemData> activeItems = new List<BeltItemData>();
    private float timeSinceLastSpawn = 0f;
    private Vector2 startPos;
    private Vector2 normalizedDir;

    // POCO class to hold data for each item on list
    private class BeltItemData
    {
        public GameObject gameObject;
        public Transform transform;
        public float distanceTraveled;
    }

    void Start()
    {
        normalizedDir = beltDirection.normalized; // normalize to get direction only
        startPos = (Vector2)transform.position + spawnOffset; // calc spawn position
    }

    // main loop
    void Update() //as far i know, it's fine if this is update instead of fixedupdate since delta time is used
    {
        MoveItems();
        HandleSpawning();
    }

    private void MoveItems()
    {
        // iterate backward to prevent skipping items on removal
        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            BeltItemData data = activeItems[i];

            if (data.gameObject == null)
            {
                activeItems.RemoveAt(i);
                continue;
            }

            // calculate movement
            float step = beltSpeed * Time.deltaTime; // calc step size
            data.transform.Translate(normalizedDir * step, Space.World); // actually move it
            data.distanceTraveled += step; // add step to POCO distance

            // ensure item within belt length
            if (data.distanceTraveled >= beltLength)
            {
                Destroy(data.gameObject);
                activeItems.RemoveAt(i);
            }
        }
    }

    private void HandleSpawning()
    {
        // safety checks
        if (activeItems.Count < maxItemsOnBelt)
        {
            timeSinceLastSpawn += Time.deltaTime;
        }

        if (timeSinceLastSpawn >= spawnInterval)
        {
            if (IsSpawnAreaClear())
            {
                SpawnItem();
                timeSinceLastSpawn = 0f;
            }
        }
    }

    private bool IsSpawnAreaClear()
    {
        // more safety checks
        if (activeItems.Count == 0) return true;

        BeltItemData lastItem = activeItems[activeItems.Count - 1];

        if (lastItem.gameObject == null) return true;

        float dist = Vector2.Distance(startPos, lastItem.transform.position);

        return dist >= minItemSpacing;
    }

    private void SpawnItem()
    {
        // ts fried me
        GearItem chosenGear = RandomItemFactory.instance.createRandomItem(); // singleton call

        //parent under this object to tidy up the inspector
        GameObject obj = Instantiate(droppedItemPrefab, startPos, Quaternion.identity, transform); // create obj

        obj.SetActive(false); // prevent null refs in awake

        // fill out checklist for dropped item
        DroppedItem droppedScript = obj.GetComponent<DroppedItem>();
        droppedScript.item = chosenGear;

        // fill out rb checklist
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;

        // ensure its visible above belt obj
        Vector3 fixPos = obj.transform.position;
        fixPos.z = transform.position.z - 0.1f;
        obj.transform.position = fixPos;

        // activate
        obj.SetActive(true);

        // add list
        activeItems.Add(new BeltItemData
        {
            gameObject = obj,
            transform = obj.transform,
            distanceTraveled = 0f
        });
    }
}
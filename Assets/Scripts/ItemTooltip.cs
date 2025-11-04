using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    public static ItemTooltip _instance;

    void Awake()
    {
        _instance = this;
    }

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI materialText;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void ShowTooltip(Item referenceItem)
    {
        Debug.Log(referenceItem);
        nameText.text = referenceItem.name;
        damageText.text = "DMG " + referenceItem.getDamage();

        materialText.text = referenceItem.material.name;
        materialText.color = referenceItem.material.color;

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}

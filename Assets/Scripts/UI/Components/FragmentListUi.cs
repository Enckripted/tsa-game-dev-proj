using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FragmentListUi : MonoBehaviour
{
    public bool ShowTextWhenNull = false;

    private FragmentInventory _fragmentInventory;
    public FragmentInventory FragmentInventory
    {
        get => _fragmentInventory; //the get accessor can only be blank in c# 14 if there's a set accessor???
        set
        {
            _fragmentInventory = value;
            UpdateText();
        }
    }

    private TextMeshProUGUI fragmentsText;

    void UpdateText()
    {
        fragmentsText.text = "None";
        if (_fragmentInventory == null || _fragmentInventory.Fragments.Count() == 0) return;

        List<string> lines = new List<string>();
        foreach (var fragmentQuantity in _fragmentInventory.Fragments)
        {
            lines.Add($"{fragmentQuantity.Type}: {fragmentQuantity.Amount}");
        }
        fragmentsText.text = string.Join("\n", lines);
    }

    void Awake()
    {
        fragmentsText = GetComponent<TextMeshProUGUI>();
    }
}

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FragmentListUi : MonoBehaviour
{
    public bool ShowTextWhenNull = false;

    private IEnumerable<FragmentQuantity> _components;
    public IEnumerable<FragmentQuantity> Components
    {
        get => _components; //the get accessor can only be blank in c# 14 if there's a set accessor???
        set
        {
            _components = value;
            UpdateText();
        }
    }

    private TextMeshProUGUI componentsText;

    void UpdateText()
    {
        componentsText.text = "None";
        if (_components == null || _components.Count() == 0) return;

        List<string> lines = new List<string>();
        foreach (var fragmentQuantity in _components)
        {
            lines.Add($"{fragmentQuantity.Type}: {fragmentQuantity.Amount}");
        }
        componentsText.text = string.Join("\n", lines);
    }

    void Awake()
    {
        componentsText = GetComponent<TextMeshProUGUI>();
    }
}

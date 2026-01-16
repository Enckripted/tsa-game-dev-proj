using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ComponentListUi : MonoBehaviour
{
    public bool ShowTextWhenNull = false;

    private IEnumerable<ComponentQuantity> _components;
    public IEnumerable<ComponentQuantity> Components
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
        foreach (var compQuant in _components)
        {
            lines.Add($"{compQuant.Type}: {compQuant.Amount}");
        }
        componentsText.text = string.Join("\n", lines);
    }

    void Awake()
    {
        componentsText = GetComponent<TextMeshProUGUI>();
    }
}

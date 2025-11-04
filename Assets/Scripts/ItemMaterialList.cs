using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemMaterial
{
    public string name;
    public Color color;
}

public class ItemMaterialList : MonoBehaviour
{
    public List<ItemMaterial> materials = new();
}

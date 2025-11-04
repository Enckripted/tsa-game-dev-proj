using UnityEngine;

[System.Serializable]
public class Item
{
	public string name;
	public int baseDamage;
	public ItemMaterial material;

	public Item(string nName, ItemMaterial nMaterial)
	{
		name = nName;
		material = nMaterial;
	}

	public int getDamage()
	{
		return baseDamage;
	}
}

using UnityEngine.UI;

/*
basic idea here is that we want to have two ways to define item:
 - stuff like inventories, where we want to simply store items and don't care about underlying data (except for name, type of item, etc.)
 - stuff like machines, where we need to access the underlying data of an object
this interface pattern handles both those cases, and GearItem, GemItem, and any other sort of item can simply derive from these
*/

public enum ItemType
{
	Gear
}

public interface Item
{
	public Image image { get; } //image type is just a placeholder until somebody finds the actual type we want to use
	public string name { get; }
	public string tooltip { get; }
	public ItemType type { get; }
}

public interface Item<T> : Item where T : ItemData
{
	public T data { get; }
}

public abstract class ItemData
{
	public abstract string genName();
	public abstract string genTooltip();
}

using System;
using Unity.VisualScripting;
using UnityEngine.UI;

[Serializable]
public class GearItem : Item<Gear>
{
	[Serialize] public Image image { get; } //placeholder type
	[Serialize] public string name { get; }
	[Serialize] public string tooltip { get; }
	[Serialize] public ItemType type { get; }

	public Gear data { get; }

	public GearItem(Gear gear)
	{
		name = gear.genName();
		tooltip = gear.genTooltip();
		type = ItemType.Gear;
		data = gear;
	}
}
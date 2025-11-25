using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GearItem : Item<Gear>
{
	[field: SerializeField] public Image image { get; private set; } //placeholder type
	[field: SerializeField] public string name { get; }
	[field: SerializeField] public string tooltip { get; }
	[field: SerializeField] public ItemType type { get; }

	public Gear data { get; }

	public GearItem(Gear gear)
	{
		name = gear.genName();
		tooltip = gear.genTooltip();
		type = ItemType.Gear;
		data = gear;
	}
}
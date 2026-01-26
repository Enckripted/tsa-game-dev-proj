using UnityEngine;
using UnityEngine.UI;

//An interface that lets us treat every time the same in containers like Inventories.
public interface IItem
{
    public ItemType Type { get; }
    public string Name { get; }
    public Tooltip HoverTooltip { get; }
    public Sprite Sprite { get; }
}

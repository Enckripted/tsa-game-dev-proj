using UnityEngine.UI;

public interface IItem
{
    public ItemType Type { get; }
    public string Name { get; }
    public Tooltip ItemTooltip { get; }
    public Image Sprite { get; }
}

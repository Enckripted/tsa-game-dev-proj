using System.IO;

public class ComponentQuantity
{
	public string type { get; private set; }
	public int amount { get; private set; }

	public ComponentQuantity(string matType, int matAmount)
	{
		type = matType;
		amount = matAmount;
	}
}
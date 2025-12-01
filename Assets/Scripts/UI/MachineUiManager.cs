using UnityEngine;

public class MachineUiManager : MonoBehaviour
{
	public static MachineUiManager instance;

	public IMachine currentMachine { get; private set; }
	private GameObject currentMachineUi;

	public void openUi(IMachine machine, GameObject uiPrefab)
	{
		if (currentMachineUi != null) return;
		currentMachine = machine;
		currentMachineUi = Instantiate(uiPrefab, transform);
	}

	public void closeUi()
	{
		currentMachine = null;
		Destroy(currentMachineUi);
	}

	void Awake()
	{
		instance = this;
	}
}
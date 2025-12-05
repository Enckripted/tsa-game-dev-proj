using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MachineUiManager : MonoBehaviour
{
	public static MachineUiManager instance;

	public IMachine currentMachine { get; private set; }

	private GameObject currentMachineUi;
	private CanvasGroup canvasGroup;
	private InputAction interactAction;
	private long machineOpenedLast;


	public void openUi(IMachine machine, GameObject uiPrefab)
	{
		if (currentMachineUi != null) return;
		currentMachine = machine;
		currentMachineUi = Instantiate(uiPrefab, transform);
		canvasGroup.blocksRaycasts = true;
		machineOpenedLast = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

		PlayerInventory.instance.inventory.targetInventory = currentMachine.inputSlots;
	}

	public void closeUi()
	{
		currentMachine = null;
		Destroy(currentMachineUi);
		canvasGroup.blocksRaycasts = false;

		//just in case
		PlayerInventory.instance.inventory.targetInventory = null;
	}

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		interactAction = InputSystem.actions.FindAction("Interact");
		instance = this;
	}

	void Update()
	{
		//has it been 50 ms since machine opened? if so, close it
		if (interactAction.WasPressedThisFrame() && currentMachineUi != null &&
		DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - machineOpenedLast > 50) closeUi();
	}
}
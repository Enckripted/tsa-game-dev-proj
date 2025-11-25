using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineUiManager : MonoBehaviour
{
	public static MachineUiManager instance;

	[SerializeField] private GameObject invSlotPrefab;
	[SerializeField] private GameObject inputSlotContainer;
	[SerializeField] private GameObject outputSlotContainer;
	[SerializeField] private GameObject durationTextObject;

	private CanvasGroup canvasGroup;
	private TextMeshProUGUI durationText;

	[field: SerializeField] public List<ItemUiDraggable> inputSlots { get; private set; }
	[field: SerializeField] public List<ItemUiDraggable> outputSlots { get; private set; }
	[field: SerializeField] public Machine currentMachine { get; private set; }

	public void loadMachine(Machine machine)
	{
		foreach (ItemUiDraggable slot in inputSlots) Destroy(slot.transform.parent.gameObject);
		foreach (ItemUiDraggable slot in outputSlots) Destroy(slot.transform.parent.gameObject);
		inputSlots.Clear();
		outputSlots.Clear();

		for (int i = 0; i < machine.inputSlots.totalSlots; i++)
		{
			ItemUiDraggable nElement = Instantiate(invSlotPrefab, inputSlotContainer.transform).GetComponentInChildren<ItemUiDraggable>();
			nElement.inventorySlot = machine.inputSlots.getSlot(i);
			inputSlots.Add(nElement);
		}
		for (int i = 0; i < machine.outputSlots.totalSlots; i++)
		{
			ItemUiDraggable nElement = Instantiate(invSlotPrefab, outputSlotContainer.transform).GetComponentInChildren<ItemUiDraggable>();
			nElement.inventorySlot = machine.outputSlots.getSlot(i);
			nElement.canDropInSlot = false;
			outputSlots.Add(nElement);
		}

		currentMachine = machine;
		openUi();
	}

	public void openUi()
	{
		canvasGroup.alpha = 1;
		canvasGroup.blocksRaycasts = true;
	}

	public void closeUi()
	{
		canvasGroup.alpha = 0;
		canvasGroup.blocksRaycasts = false;
	}

	void Awake()
	{
		instance = this;
		canvasGroup = GetComponent<CanvasGroup>();
		durationText = durationTextObject.GetComponent<TextMeshProUGUI>();
		closeUi();
	}

	void Update()
	{
		if (canvasGroup.alpha == 0) return; //if it is enabled then a machine has been selected

		if (currentMachine.running) durationText.text = string.Format("{0:0.0}", currentMachine.secondsRemaining) + "s";
		else durationText.text = "0.0s";
	}
}

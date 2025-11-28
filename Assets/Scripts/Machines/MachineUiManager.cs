using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MachineUiManager : MonoBehaviour
{
	public static MachineUiManager instance { get; private set; }

	[SerializeField] private GameObject invSlotPrefab;
	[SerializeField] private GameObject inputSlotContainer;
	[SerializeField] private GameObject outputSlotContainer;
	[SerializeField] private TextMeshProUGUI durationText;
	[SerializeField] private TextMeshProUGUI componentCostText;
	[SerializeField] private Image startButtonSprite;
	[SerializeField] private Button startButton;
	[SerializeField] private Button closeButton;

	private CanvasGroup canvasGroup;

	[field: SerializeField] public List<ItemUiDraggable> inputSlots { get; private set; }
	[field: SerializeField] public List<ItemUiDraggable> outputSlots { get; private set; }
	[field: SerializeField] public IMachine currentMachine { get; private set; }

	private string getCostText(IEnumerable<ComponentQuantity> compQuants)
	{
		string current = "";
		foreach (ComponentQuantity compQuant in compQuants)
		{
			current += compQuant.amount + " " + compQuant.type + "\n";
		}
		return current;
	}

	public void loadMachine(IMachine machine)
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

		startButton.transform.parent.gameObject.SetActive(!machine.runsAutomatically);

		currentMachine = machine;
		openUi();
	}

	private void openUi()
	{
		canvasGroup.alpha = 1;
		canvasGroup.blocksRaycasts = true;
	}

	private void closeUi()
	{
		canvasGroup.alpha = 0;
		canvasGroup.blocksRaycasts = false;
	}

	void Awake()
	{
		instance = this;
		canvasGroup = GetComponent<CanvasGroup>();

		startButton.onClick.AddListener(() => { currentMachine.attemptMachineStart(); });

		closeButton.onClick.AddListener(closeUi);
		closeUi();
	}

	void Update()
	{
		if (canvasGroup.alpha == 0) return; //if it is enabled then a machine has been selected

		//there's an issue where the button still fades a bit when you mouse over it but im not gonna bother fixing that till we do a proper ui design
		if (!currentMachine.running && currentMachine.hasValidRecipe())
		{
			startButtonSprite.color = new Color(1, 1, 1);
			componentCostText.text = getCostText(currentMachine.currentRecipe.Value.componentInputs);
		}
		else
		{
			startButtonSprite.color = new Color(0.5f, 0.5f, 0.5f);
			componentCostText.text = "";
		}

		if (currentMachine.running) durationText.text = string.Format("{0:0.0}", currentMachine.secondsRemaining) + "s";
		else durationText.text = "0.0s";
	}
}

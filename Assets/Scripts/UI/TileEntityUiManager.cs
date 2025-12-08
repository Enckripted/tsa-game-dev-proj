using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileEntityUiManager : MonoBehaviour
{
	[SerializeField] private int msUntilUiCanClose = 50;

	public static TileEntityUiManager instance;

	private CanvasGroup canvasGroup;
	private InputAction interactAction;
	private ITileEntity currentTileEntity;
	private GameObject currentUiInstance;
	private RectTransform currentUiTransform;
	private long timeLastOpened;

	public void openUi(ITileEntity tileEntity, GameObject uiInstance)
	{
		if (currentUiInstance != null) //this code block should never run
		{
			Destroy(uiInstance);
			return;
		}
		currentTileEntity = tileEntity;
		currentUiInstance = uiInstance;
		currentUiInstance.transform.SetParent(transform.parent, false);

		currentUiTransform = currentUiInstance.GetComponent<RectTransform>();
		currentUiTransform.anchoredPosition = Vector2.zero;

		currentTileEntity.loadUi(currentUiInstance);
		canvasGroup.blocksRaycasts = true;
		timeLastOpened = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}

	public void closeUi()
	{
		currentTileEntity.unloadUi(currentUiInstance);
		Destroy(currentUiInstance);
		canvasGroup.blocksRaycasts = false;
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
		if (interactAction.WasPressedThisFrame() && currentUiInstance != null &&
		DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - timeLastOpened > msUntilUiCanClose) closeUi();
	}
}
using UnityEngine;

public interface ITileEntity
{
    public GameObject uiPrefab { get; }

    public void loadUi(GameObject uiInstance);
    public void unloadUi(GameObject uiInstance);
}

[RequireComponent(typeof(Interactable))]
public abstract class TileEntity : MonoBehaviour, ITileEntity
{
    //a set has to be there for it to serialize
    public abstract GameObject uiPrefab { get; protected set; }

    private Interactable interactable;

    public abstract void loadUi(GameObject uiInstance);
    public abstract void unloadUi(GameObject uiInstance);
    protected abstract void onStart();

    private GameObject instantiateUi()
    {
        return Instantiate(uiPrefab);
    }

    private void openPrefabUi()
    {
        GameObject uiInstance = instantiateUi();
        TileEntityUiManager.instance.openUi(this, uiInstance);
    }

    void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        interactable.interactEvent.AddListener(openPrefabUi);
        onStart();
    }
}

using UnityEngine;

public interface ITileEntity
{
    public GameObject UiPrefab { get; }

    public void LoadUi(GameObject uiInstance);
    public void UnloadUi(GameObject uiInstance);
}

[RequireComponent(typeof(Interactable))]
public abstract class TileEntity : MonoBehaviour, ITileEntity
{
    //a set has to be there for it to serialize
    public abstract GameObject UiPrefab { get; protected set; }

    private Interactable interactable;

    public abstract void LoadUi(GameObject uiInstance);
    public abstract void UnloadUi(GameObject uiInstance);
    protected abstract void OnStart();

    private GameObject InstantiateUi()
    {
        return Instantiate(UiPrefab);
    }

    private void OpenPrefabUi()
    {
        GameObject uiInstance = InstantiateUi();
        TileEntityUiManager.Instance.OpenUi(this, uiInstance);
    }

    void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        interactable.InteractEvent.AddListener(OpenPrefabUi);
        OnStart();
    }
}

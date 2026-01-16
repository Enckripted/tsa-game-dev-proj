using UnityEngine;

public abstract class TileEntity : Entity, ITileEntity
{
    //a set has to be there for it to serialize
    public abstract GameObject UiPrefab { get; protected set; }

    public abstract void LoadUi(GameObject uiInstance);
    public abstract void UnloadUi(GameObject uiInstance);

    private GameObject InstantiateUi()
    {
        return Instantiate(UiPrefab);
    }

    private void OpenPrefabUi()
    {
        GameObject uiInstance = InstantiateUi();
        TileEntityUiManager.Instance.OpenUi(this, uiInstance);
    }

    protected override void OnInteract()
    {
        OpenPrefabUi();
    }
}

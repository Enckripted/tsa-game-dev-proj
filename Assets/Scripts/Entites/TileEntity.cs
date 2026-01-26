using UnityEngine;


//Implements the Position and UI attributes from the interface, and also has code to load UIs into
//the TileEntityUiManager to avoid repetition across the codebase.
[RequireComponent(typeof(AudioSource))]
public abstract class TileEntity : Entity, ITileEntity
{
    //a set has to be there for it to serialize
    public abstract GameObject UiPrefab { get; protected set; }
    public Vector2 Position { get; } //temporary for in case we ever add machine placement :)

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

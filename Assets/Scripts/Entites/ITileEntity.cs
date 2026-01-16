using UnityEngine;

public interface ITileEntity
{
    public GameObject UiPrefab { get; }

    public void LoadUi(GameObject uiInstance);
    public void UnloadUi(GameObject uiInstance);
}

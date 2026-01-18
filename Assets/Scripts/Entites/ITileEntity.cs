using UnityEngine;


//We need an interface for TileEntites because there's some case where we want to handle all
//different types like TileEntityUiManager and placing down machines if we ever get to that
public interface ITileEntity
{
    public GameObject UiPrefab { get; }
    public Vector2 Position { get; }

    public void LoadUi(GameObject uiInstance);
    public void UnloadUi(GameObject uiInstance);
}

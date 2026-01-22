using UnityEngine;

public class GameColors : MonoBehaviour
{
    public static GameColors Instance;

    public Color GoldColor;
    public Color NameReforgeColor;
    public Color GemSlotColor;

    void Awake()
    {
        Instance = this;
    }
}

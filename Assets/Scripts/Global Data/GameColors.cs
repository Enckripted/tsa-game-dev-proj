using UnityEngine;

public class GameColors : MonoBehaviour
{
    public static GameColors Instance;

    public Color GoldColor;
    public Color NameReforgeColor;

    void Awake()
    {
        Instance = this;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpriteService : MonoBehaviour
{
    [SerializeField] private Color maskColor;
    private static Dictionary<string, Dictionary<string, Sprite>> sprites;

    public static Sprite GetItemSpriteFor(string baseName, Material material)
    {
        if (!sprites.ContainsKey(baseName)) throw new Exception("Tried to access non-existent sprite item type " + baseName);
        if (!sprites[baseName].ContainsKey(material.Name)) throw new Exception("Tried to access non-existent material sprite "
            + material.Name + " for sprite item type " + baseName);
        return sprites[baseName][material.Name];
    }

    void Start()
    {
        List<Texture2D> itemTextures = new List<Texture2D>(Resources.LoadAll<Texture2D>("Art/Items"));
        sprites = new Dictionary<string, Dictionary<string, Sprite>>();

        foreach (Texture2D itemTexture in itemTextures)
        {
            sprites.Add(itemTexture.name, new Dictionary<string, Sprite>());

            foreach (MaterialScriptableObject materialData in ScriptableObjectData.BaseMaterials)
            {
                Texture2D texture = TextureColorerService.ColorTexture(itemTexture, maskColor, materialData.Color);
                Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                sprites[itemTexture.name].Add(materialData.Name, sprite);
            }
        }
    }
}

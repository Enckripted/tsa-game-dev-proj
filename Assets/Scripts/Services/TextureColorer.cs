using System;
using UnityEngine;

public static class TextureColorerService
{
    //doesn't need to be an argument for now
    private const double Tolerance = 0.05;

    private static bool PartOfMask(Color pixel, Color mask)
    {
        return Vector3.Distance(
            new Vector3(pixel.r, pixel.g, pixel.b),
            new Vector3(mask.r, mask.g, mask.b)
        ) < Tolerance;
    }

    public static Texture2D ColorTexture(Texture2D texture, Color maskColor, Color newColor)
    {
        Texture2D nTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false, false);

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color pixel = texture.GetPixel(x, y);
                if (PartOfMask(pixel, maskColor))
                {
                    Color color = new Color(newColor.r, newColor.g, newColor.b, pixel.a);
                    nTexture.SetPixel(x, y, color);
                }
                else
                {
                    nTexture.SetPixel(x, y, pixel);
                }
            }
        }

        nTexture.Apply();
        return nTexture;
    }
}

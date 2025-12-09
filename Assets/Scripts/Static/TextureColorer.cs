using System;
using UnityEngine;

public static class TextureColorer
{
	//doesn't need to be an argument for now
	private const double tolerance = 0.05;

	private static bool partOfMask(Color pixel, Color mask)
	{
		return Vector3.Distance(
			new Vector3(pixel.r, pixel.g, pixel.b),
			new Vector3(mask.r, mask.g, mask.b)
		) < tolerance;
	}

	public static Texture2D colorTexture(Texture2D texture, Color maskColor, Color newColor)
	{
		Texture2D nTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false, false);

		for (int y = 0; y < texture.height; y++)
		{
			for (int x = 0; x < texture.width; x++)
			{
				Color pixel = texture.GetPixel(x, y);
				if (partOfMask(pixel, maskColor))
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
		/*
		Color[] pixels = texture.GetPixels();

		for (int i = 0; i < pixels.Length; i++)
		{
			Color pixel = pixels[i];
			if (Vector3.Distance(
				new Vector3(pixel.r, pixel.g, pixel.b),
				new Vector3(maskColor.r, maskColor.g, maskColor.b)
			) < tolerance)
			{
				pixels[i] = new Color(Mathf.LinearToGammaSpace(newColor.r), Mathf.LinearToGammaSpace(newColor.b), Mathf.LinearToGammaSpace(newColor.g), pixel.a);
			}
		}
		*/


		//Debug.Log(texture.format);
		//Debug.Log(nTexture.format);
		//nTexture.SetPixels(pixels);
		nTexture.Apply();
		return nTexture;
	}
}
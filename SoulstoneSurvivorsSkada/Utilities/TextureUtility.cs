using UnityEngine;

namespace SoulstoneSurvivorsSkada;

public class TextureUtility
{
	/// <summary>
	/// Texture for the Bar
	/// </summary>
	public static Texture2D BarTexture
	{
		get
		{
			// create a 1x1 texture with red color
			Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
			// set the pixel to red
			texture.SetPixel(0, 0, Color.red);
			texture.wrapMode = TextureWrapMode.Repeat;
			// apply the changes
			texture.Apply();
			texture.hideFlags = HideFlags.DontSave;
			// return the texture
			return texture;
		}
	}
}

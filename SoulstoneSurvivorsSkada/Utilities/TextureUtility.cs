using UnityEngine;

namespace SoulstoneSurvivorsSkada;

public class TextureUtility
{
	private static readonly Color barColorA = new Color(0.2f, 0.2f, 0.2f);
	private static readonly Color barColorB = new Color(0.42f, 0.42f, 0.42f);
	
	/// <summary>
	/// Texture for the Bar
	/// </summary>
	public static Texture2D BarTexture
	{
		get
		{
			// create a 1x1 texture with red color
			Texture2D texture = new Texture2D(1, 30, TextureFormat.RGBA32, false);
			for (int i = 0; i < 30; i++)
			{
				texture.SetPixel(0, i, Color.Lerp(barColorA, barColorB, i / 30f));
			}
			texture.wrapMode = TextureWrapMode.Clamp;
			texture.filterMode = FilterMode.Point;
			// apply the changes
			texture.Apply();
			texture.hideFlags = HideFlags.DontSave;
			// return the texture
			return texture;
		}
	}
}

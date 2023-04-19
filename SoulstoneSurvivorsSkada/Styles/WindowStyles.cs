using UnityEngine;

namespace SoulstoneSurvivorsSkada.Styles;

public static class WindowStyles
{

	public static readonly GUIStyle ModalStyle = new (GUI.skin.window)
	{
		normal =
		{
			background = CreateBackground(Color.black)
		}
	};
	
	public static Texture2D CreateBackground(Color color)
	{
		// create a 1x1 texture with red color
		Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
		texture.SetPixel(0, 0, color);
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.filterMode = FilterMode.Point;
		// apply the changes
		texture.Apply();
		texture.hideFlags = HideFlags.DontSave;
		// return the texture
		return texture;
	}
}
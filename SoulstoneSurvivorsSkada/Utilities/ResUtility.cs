using UnityEngine;

namespace SoulstoneSurvivorsSkada;

public static class ResUtility
{
	private const float DefaultWidth = 1920f;
	private const float DefaultHeight = 1080f;
	
	public static float GetWidth(float width)
	{
		return width * (Screen.width / DefaultWidth);
	}
	
	public static float GetHeight(float height)
	{
		return height * (Screen.height / DefaultHeight);
	}

	public static int GetFontSize(int i)
	{
		return (int) (i * (Screen.height / DefaultHeight));
	}
}
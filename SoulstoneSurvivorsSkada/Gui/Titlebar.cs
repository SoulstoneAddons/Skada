using System.Collections.Generic;
using SoulstoneSurvivorsSkada.Gui.Elements;
using SoulstoneSurvivorsSkada.Gui.Interfaces;
using SoulstoneSurvivorsSkada.Styles;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Gui;

public class Titlebar
{
	private readonly Stack<ITitlebarElement> _elements;

	public Titlebar(Stack<ITitlebarElement> elements)
	{
		_elements = elements;
	}

	private static readonly GUIStyle TitlebarButtonStyle = new(GUI.skin.button)
	{
		active =
		{
			background = WindowStyles.CreateBackground(new Color(0.34f, 0.34f, 0.34f))
		},
		normal =
		{
			background = WindowStyles.CreateBackground(new Color(0.45f, 0.45f, 0.45f))
		},
	};

	public void Draw(ref Rect windowRect)
	{
		float x = windowRect.width - 5;
		foreach (ITitlebarElement element in _elements)
		{
			switch (element)
			{
				case TitlebarButton button:
				{
					// calculate size of button
					Vector2 size = TitlebarButtonStyle.CalcSize(button.Content);
					x -= size.x;
					if (GUI.Toggle(new Rect(x, 0, size.x, ResUtility.GetHeight(20)), 
						    button.State,
						    button.Content, 
						    TitlebarButtonStyle))
					{
						
						button.Action();
					}

					break;
				}
			}
		}
	}
}
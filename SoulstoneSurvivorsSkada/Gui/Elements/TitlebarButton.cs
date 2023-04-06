using System;
using SoulstoneSurvivorsSkada.Gui.Interfaces;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Gui.Elements;

public struct TitlebarButton : ITitlebarElement
{
	public GUIContent Content { get; }
	public Action Action { get; }

	public TitlebarButton(GUIContent content, Action action)
	{
		Content = content;
		Action = action;
	}
}
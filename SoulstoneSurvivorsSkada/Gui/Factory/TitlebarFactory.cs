using System;
using System.Collections.Generic;
using SoulstoneSurvivorsSkada.Gui.Elements;
using SoulstoneSurvivorsSkada.Gui.Interfaces;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Gui;

public struct TitlebarFactory
{
	private Stack<ITitlebarElement> _elements;
	
	public TitlebarFactory()
	{
		_elements = new Stack<ITitlebarElement>();
	}

	public TitlebarFactory AddButton(string text, Action action)
	{
		_elements.Push(new TitlebarButton(new GUIContent(text), action));
		return this;
	}

	public Titlebar Build()
	{
		return new Titlebar(_elements);
	}
}
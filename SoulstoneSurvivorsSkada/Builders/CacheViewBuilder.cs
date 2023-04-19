using System;
using System.Collections.Generic;
using SoulstoneSurvivorsSkada.Gui;
using SoulstoneSurvivorsSkada.Interfaces;

namespace SoulstoneSurvivorsSkada.Builders;

public readonly ref struct CacheViewBuilder
{
	public CacheViewBuilder()
	{
		_views = new Stack<ISkadaView>();
	}

	private readonly Stack<ISkadaView> _views;

	public CacheViewBuilder AddView<T>()
		where T : ISkadaView
	{
		_views.Push(Activator.CreateInstance<T>());
		return this;
	}
	
	public void BuildCache()
	{
		foreach (ISkadaView view in _views)
		{
			Window.Views.Add(view.GetType(), view);
		}
	}
}
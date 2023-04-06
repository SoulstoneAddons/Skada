using System;
using System.Collections.Generic;
using SoulstoneSurvivorsSkada.Interfaces;

namespace SoulstoneSurvivorsSkada.Views;

public static class Window
{
	// backing field for current view property
	// property can be used later to have callbacks for view changes
	private static ISkadaView _currentView;
	
	/// <summary>
	/// Current View
	/// </summary>
	public static ISkadaView CurrentView => _currentView;

	private static readonly Dictionary<Type, ISkadaView> Views = new();
	
	/// <summary>
	/// Changes the View to the given Type
	/// </summary>
	/// <typeparam name="T">View</typeparam>
	public static void ChangeView<T>() 
		where T : ISkadaView
	{
		// check if view is already cached
		if (Views.TryGetValue(typeof(T), out ISkadaView view))
		{
			// set the current view to the cached view
			_currentView?.OnDisable();
			_currentView = view;
			view?.OnEnable();
			return; // return early to avoid creating a new instance
		}
		// create a new instance of the view
		ISkadaView instance = Activator.CreateInstance<T>();
		// cache the view
		Views.Add(typeof(T), instance);
		_currentView?.OnDisable();
		// set the current view to the new instance
		_currentView = instance;
		instance?.OnEnable();
	}
}
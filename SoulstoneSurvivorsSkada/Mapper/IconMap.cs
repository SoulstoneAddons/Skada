using System.Collections.Generic;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Mapper;

/// <summary>
/// Map of icons for the power-ups
/// </summary>
public static class IconMap
{
	/// <summary>
	/// Dictionary of icons for the power-ups
	/// </summary>
	public static IReadOnlyDictionary<string, Texture2D> Icons { get; private set; }

	public static int LoadIcons()
	{
		// check if the icons are already loaded and return the count
		if (Icons != null) return Icons.Count;
		// create a new dictionary
		var dict = new Dictionary<string, Texture2D>();
		
		// load all the icons from the power-ups-icon folder
		Il2CppArrayBase<Texture2D> icons = Resources.LoadAll<Texture2D>("power-ups-icon");
		// initialize the loaded count to 0
		int loaded = 0;
		// loop through all the icons
		foreach (Texture2D icon in icons)
		{
			// set the hide flags to hide and don't save to prevent it from being collected by the garbage collector
			icon.hideFlags = HideFlags.HideAndDontSave;
			
			// convert the name to lowercase and add it to the dictionary
			string name = icon.name.ToLower();
			dict[name] = icon;
			
			// increment the loaded count
			loaded++;
		}
		// set the icons to the dictionary
		Icons = dict;
		
		// return the loaded count
		return loaded;
	}
}
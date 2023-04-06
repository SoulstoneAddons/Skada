using System;
using BepInEx.Logging;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;
using SoulstoneSurvivorsSkada.Logging;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace SoulstoneSurvivorsSkada.Mapper;

public static class IconMap
{
	public static Dictionary<string, Texture2D> Icons { get; } = new();

	public static void LoadIcons()
	{
		Il2CppArrayBase<Texture2D> icons = Resources.LoadAll<Texture2D>(string.Empty);
		foreach (Texture2D icon in icons)
		{
			icon.hideFlags = HideFlags.HideAndDontSave;
			if (Icons.ContainsKey(icon.name)) continue;
			
			Icons.Add(icon.name, icon);
		}
	}
}
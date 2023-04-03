using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using BepInEx.Logging;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.IO;
using SoulstoneSurvivorsSkada.Logging;
using SoulstoneSurvivorsSkada.Patchers;
using SoulstoneSurvivorsSkada.Views;
using UnityEngine;

namespace SoulstoneSurvivorsSkada;

/// <summary>
/// Bootstrapper for the plugin to access the game
/// </summary>
internal sealed class Bootstrapper : MonoBehaviour
{
	// Required for BepInEx
	public Bootstrapper(IntPtr ptr) : base(ptr)
	{
	}

	/// <summary>
	/// Called whenever the component is enabled
	/// </summary>
	private void OnEnable()
	{
		// Patch all the things with Harmony
		SkadaPlugin.Harmony.PatchAll(typeof(Bootstrapper));
		SkadaPlugin.Harmony.PatchAll(typeof(GamePatches));
		
		// Change damage meter view to default view
		// Any type that inherits ISkadaView can be used
		// the instances are cached for performance and reusability
		Window.ChangeView<PlayerDamageMeterSkadaView>();
	}

	// Window rectangle for the damage meter
	// starting position
	private static Rect windowRect = new(Screen.width - 320 * (Screen.width / 1920.0f), 
		Screen.height / 2.0f, 
		ResUtility.GetWidth(320),
		ResUtility.GetHeight(240));


	private void OnGUI()
	{
		// check if game manager is loaded
		if (!GameManagerUtil.HasGameManager) 
			return;
		
		// if any of these are null
		if (GameManagerUtil.GameplayUIManager.PausePanel.IsPaused)
			return;
		
		// set the background color of Window to black for eye candy
		GUI.backgroundColor = Color.black;

		// create the window of damage meter
		windowRect = GUI.Window(0,
			windowRect,
			(GUI.WindowFunction)DrawSkadaWindow,
			Window.CurrentView.Title);
	}

	private void Update()
	{
		// check if game manager is loaded
		if (!GameManagerUtil.HasGameManager) 
			return;
		// if any of these are null
		if (GameManagerUtil.GameplayUIManager.PausePanel.IsPaused)
			return;
		
		// every second order the spells by damage to reduce the calls
		if (Time.frameCount % 60 == 0)
		{
			// order the spells by damage in ascending order
			PlayerSkadaHistory.DamageBySpellsOrdered = PlayerSkadaHistory.SortDamageBySpellsOrdered();
		}
	}

	// Make the contents of the window
	private static void DrawSkadaWindow(int windowID)
	{
		// Make a very long rect that is 20 pixels tall.
		// This will make the window be resizable by the top
		// title bar - no matter how wide it gets.
		GUI.DragWindow(new Rect(0, 0, 10000, ResUtility.GetHeight(20)));
		
		// get the scroll position
		int scrollPosition = Window.CurrentView.ScrollPosition;

		// if the scroll position is not 0 then scroll the window
		if (ScrollUtility.Scroll(windowRect.height,
			    ResUtility.GetHeight(20),
			    ref scrollPosition, out _))
		{
			// set the scroll position
			Window.CurrentView.ScrollPosition = scrollPosition;
		}
		
		// render the current view
		Window.CurrentView.OnGUI(ref windowRect, windowID);
	}

	// Patch the cursor so when you hover over the window it does not use in-game cursor
	[HarmonyPatch(typeof(Cursor), nameof(Cursor.SetCursor))]
	[HarmonyPatch(new []{ typeof(Texture2D), typeof(Vector2), typeof(CursorMode) })]
	[HarmonyPrefix]
	public static void SetCursorPrefix(ref Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
	{
		// If mouse is over the window then set the cursor to null
		if (windowRect.Contains(Event.current.mousePosition))
		{
			// set the cursor to null
			texture = null;
		}
	}
}
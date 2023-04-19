using System;
using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using SoulstoneSurvivorsSkada.Constants;
using SoulstoneSurvivorsSkada.Gui;
using SoulstoneSurvivorsSkada.Logging;
using SoulstoneSurvivorsSkada.Mapper;
using SoulstoneSurvivorsSkada.Patchers;
using SoulstoneSurvivorsSkada.Views;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

[assembly: MelonInfo(typeof(SoulstoneSurvivorsSkada.SkadaPlugin), "Skada Plugin", "1.0.0", "Buddy")]

namespace SoulstoneSurvivorsSkada;
internal sealed class SkadaPlugin : MelonMod
{
	private static Titlebar titlebar;
	
	/// <summary>
	/// Runs when the Melon is registered. Executed before the Melon's info is printed to the console. This callback should only be used a constructor for the Melon.
	/// </summary>
	/// <remarks>
	/// Please note that this callback may run before the Support Module is loaded.
	/// <br>As a result, using unhollowed assemblies may not be possible yet and you would have to override <see cref="M:MelonLoader.MelonBase.OnInitializeMelon" /> instead.</br>
	/// </remarks>
	public override void OnEarlyInitializeMelon()
	{
		LogManager.SetLogger(LoggerInstance);
		base.OnEarlyInitializeMelon();
	}

	/// <summary>
	/// Runs after the Melon has registered. This callback waits until MelonLoader has fully initialized (<see cref="F:MelonLoader.MelonEvents.OnApplicationStart" />).
	/// </summary>
	public override void OnInitializeMelon()
	{
		base.OnInitializeMelon();
		int loaded = IconMap.LoadIcons();
		LoggerInstance.Msg($"Icon Map Loaded: {loaded} icons available.");
		HarmonyInstance.PatchAll(typeof(SkadaPlugin));
		HarmonyInstance.PatchAll(typeof(GamePatches));
	}


	/// <summary>
	/// Runs after <see cref="M:MelonLoader.MelonBase.OnInitializeMelon" />. This callback waits until Unity has invoked the first 'Start' messages (<see cref="F:MelonLoader.MelonEvents.OnApplicationLateStart" />).
	/// </summary>
	public override void OnLateInitializeMelon()
	{
		base.OnLateInitializeMelon();
		// Patch all the things with Harmony

		// Build the cache for the views
		// this is for ShiftView to work, otherwise it does not detect that there are more than one view.
		Window.CacheBuilder()
			.AddView<PlayerDamageMeterSkadaView>()
			.AddView<PlayerDpsSkadaView>()
			.AddView<PlayerDamageTakenSkadaView>()
			.BuildCache();
		
		// Create the titlebar for Skada window
		titlebar = new TitlebarFactory()
			.AddButton("DPS", Window.ChangeView<PlayerDamageMeterSkadaView>) // Change view to DPS
			.AddButton("SDPS", Window.ChangeView<PlayerDpsSkadaView>)        // Change view to Spell DPS
			.AddButton("DT", Window.ChangeView<PlayerDamageTakenSkadaView>) // Change view to Damage Taken")
			.Build();
		
		LogManager.Log(LogLevel.Info, "Skada OnEnable()");
		
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

	/// <summary>
	/// Can run multiple times per frame. Mostly used for Unity's IMGUI.
	/// </summary>
	public override void OnGUI()
	{
		base.OnGUI();
		// check if game manager is loaded
		if (!GameManagerUtil.HasGameManager) 
			return;
		
		// set the background color of Window to black for eye candy
		GUI.backgroundColor = Color.black;
		GUI.skin.window.alignment = TextAnchor.UpperLeft;
		
		// create the window of damage meter
		windowRect = GUI.Window(WindowState.Main,
			windowRect,
			(GUI.WindowFunction)DrawSkadaWindow,
			Window.CurrentView.Title,
			GUI.skin.window);
	}

	/// <summary>Runs once per frame.</summary>
	public override void OnUpdate()
	{
		base.OnUpdate();
		// check if game manager is loaded
		if (!GameManagerUtil.HasGameManager) 
			return;
		
		Window.CurrentView?.Update();
		
		// check if keybind is pressed
		if (Keyboard.current.f6Key.wasPressedThisFrame)
		{
			LogManager.Log(LogLevel.Info, "Toggle window");
			WindowState.ToggleWindow(WindowState.Settings);
		}

		if (Keyboard.current.tabKey.wasPressedThisFrame)
		{
			LogManager.Log(LogLevel.Info, "Shift view");
			Window.ShiftView();
		}

		// called every 60th frame
		if (Time.frameCount % 60 == 0)
		{
			Window.CurrentView?.LateUpdate();

			if (PlayerSkadaHistory.IsEmpty)
			{
				PlayerSkadaHistory.Reset();
			}
		}
	}
	
	private static void DrawModalWindow(int id)
	{
		if (GUILayout.Button("Full reset"))
		{
			PlayerSkadaHistory.Reset();
			PlayerSkadaHistory.ClearDamageBySpells();
			PlayerSkadaHistory.Start();
		}
	}
	
	
	// Make the contents of the window
	private static void DrawSkadaWindow(int windowID)
	{
		titlebar.Draw(ref windowRect);
		
		// Make a very long rect that is 20 pixels tall.
		// This will make the window be resizable by the top
		// title bar - no matter how wide it gets.
		GUI.DragWindow(new Rect(0, 0, 10000, ResUtility.GetHeight(20)));

		// get the scroll position
		int scrollPosition = Window.CurrentView.ScrollPosition;

		// if the scroll position is not 0 then scroll the window
		if (ScrollUtility.Scroll(windowRect.height,
			    ResUtility.GetHeight(30),
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
using System;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.Linq;
using SoulstoneSurvivorsSkada.Gui;
using SoulstoneSurvivorsSkada.Interfaces;
using SoulstoneSurvivorsSkada.Logging;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Views;

public class PlayerDamageTakenSkadaView : ISkadaView
{
	/// <summary>
	/// Position of the Scrollbar
	/// </summary>
	public int ScrollPosition { get; set; }

	/// <summary>
	/// Title of the View
	/// </summary>
	public string Title { get; set; } = "Skada - Player Damage Taken";

	/// <summary>
	/// Called by the GUI
	/// </summary>
	/// <param name="windowRect">Rectangle of the Window</param>
	/// <param name="windowID">ID of the Window</param>
	public void OnGUI(ref Rect windowRect, int windowID)
	{
		GUILayout.BeginVertical();
		{
			System.Collections.Generic.Dictionary<string, DamageLog> spells =
				PlayerDamageTakenSkadaHistory.DamageTaken;
			foreach (DamageLog spell in spells.Values)
			{
				SkadaBar.RenderBarPercent(ref windowRect, 
					ScrollPosition,
					spell, 
					PlayerDamageTakenSkadaHistory.TotalDamageTaken);
			}
			
		}
		GUILayout.EndVertical();
	}

	/// <summary>
	/// Called when the View is active
	/// </summary>
	public void OnActivated()
	{
	}

	/// <summary>
	/// Called when the View is deactivated
	/// </summary>
	public void OnDeactivated()
	{
		
	}

	/// <summary>
	/// Called every frame when the View is active
	/// </summary>
	public void Update()
	{
	}
	
	public void LateUpdate()
	{
		
	}
}
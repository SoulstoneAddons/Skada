using SoulstoneSurvivorsSkada.Extensions;
using SoulstoneSurvivorsSkada.Interfaces;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Views;

public sealed class PlayerDpsSkadaView : ISkadaView
{
	/// <summary>
	/// Position of the Scrollbar
	/// </summary>
	public int ScrollPosition { get; set; }

	/// <summary>
	/// Title of the View
	/// </summary>
	public string Title { get; set; } = "Skada - Player DPS";

	/// <summary>
	/// Called by the GUI
	/// </summary>
	/// <param name="windowRect">Rectangle of the Window</param>
	/// <param name="windowID">ID of the Window</param>
	public void OnGUI(ref Rect windowRect, int windowID)
	{
		// TODO implement view logic
		// Test view
		float dps = PlayerSkadaHistory.PlayerDps;
		GUILayout.Label($"DPS: {dps.ToHumanReadableString()}");
	}
}
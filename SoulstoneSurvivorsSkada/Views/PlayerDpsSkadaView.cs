using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using SoulstoneSurvivorsSkada.Arrays;
using SoulstoneSurvivorsSkada.Extensions;
using SoulstoneSurvivorsSkada.Gui;
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
		// save spells into a variable to avoid multiple calls
		
		Il2CppReferenceArray<GameStatsSkillData> spells = PlayerSkadaHistory.DpsBySpellsOrdered;
		
		GUILayout.BeginVertical();
		{
			float time = Time.time;
			float dps = PlayerSkadaHistory.CalculatePlayerDps(time);
			
			GUILayout.Label($"DPS: {PlayerSkadaHistory.PlayerDps.ToHumanReadableString()}");

			// set start index to scroll position
			// run until the end of the list
			for (int i = ScrollPosition; i < spells.Count; i++)
			{
				// Get the spell data based on index
				GameStatsSkillData skillData = spells[i];
				// if the spell name hash is 0, skip it
				if (skillData.SkillNameHash == 0) continue;
				// Render the bar for the spell based on DPS
				SkadaBar.RenderBarDPS(ref windowRect,
					i,
					skillData,
					dps,
					time);
			}
		}
		GUILayout.EndVertical();
	}
	
	public void OnActivated()
	{
		
	}

	/// <summary>
	/// Called when the View is disabled
	/// </summary>
	public void OnDeactivated()
	{
	}

	public void Update()
	{
	}

	/// <summary>
	/// Called when the Damage is updated
	/// </summary>
	public void LateUpdate()
	{
		// Sort the spells by DPS
		ArraySorter.Sort(ref PlayerSkadaHistory.DpsBySpellsOrdered, 
			(data, skillData) => data.GetDps().CompareTo(skillData.GetDps()));
	}
}
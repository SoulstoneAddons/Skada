using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using SoulstoneSurvivorsSkada.Arrays;
using SoulstoneSurvivorsSkada.Gui;
using SoulstoneSurvivorsSkada.Interfaces;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Views;

public sealed class PlayerDamageMeterSkadaView : ISkadaView
{

	public int ScrollPosition { get; set; }
	public string Title { get; set; } = "Skada - Player Damage";

	public void OnActivated()
	{
		
	}

	/// <summary>
	/// Called when the View is deactivated
	/// </summary>
	public void OnDeactivated()
	{
		
	}

	public void Update()
	{
	}

	/// <summary>
	/// Called each second
	/// </summary>
	public void LateUpdate()
	{
		// Sort the spells by damage
		ArraySorter.Sort(ref PlayerSkadaHistory.DamageBySpellsOrdered, 
			(data, skillData) => data.FloatValue.CompareTo(skillData.FloatValue));
	}

	public void OnGUI(ref Rect windowRect, int windowID)
	{
		GUILayout.BeginVertical();
		{
			Il2CppReferenceArray<GameStatsSkillData> spells = PlayerSkadaHistory.DamageBySpellsOrdered;
			
			// set start index to scroll position
			// run until the end of the list
			for (int i = ScrollPosition; i < spells.Count; i++)
			{
				// Get the spell data based on index
				GameStatsSkillData skillData = spells[i];
				// if the spell name hash is 0, skip it
				if (skillData.SkillNameHash == 0) continue;
				// Render the bar for the spell based on total damage
				SkadaBar.RenderBarPercent(ref windowRect, 
					i, 
					skillData, 
					PlayerSkadaHistory.PlayerTotalDamage);
			}
		}
		GUILayout.EndVertical();
	}
}
using System;
using System.IO;
using System.Linq;
using System.Text;
using BepInEx.Logging;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace SoulstoneSurvivorsSkada;

/// <summary>
/// History of the fight for the player
/// </summary>
internal static class PlayerSkadaHistory
{
	/// <summary>
	/// If the history is empty or not
	/// </summary>
	public static bool IsEmpty => GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNew.Count == 0;
	
	public static bool IsStarted => SkadaTime.PlayerStartTime > 0;
	
	/// <summary>
	/// Duration of the fight in seconds
	/// </summary>
	public static float FightDuration => Time.time - SkadaTime.PlayerStartTime;
	
	/// <summary>
	/// Get the total damage done by the player per spell
	/// </summary>
	public static Il2CppReferenceArray<GameStatsSkillData> DamageBySpells => 
		GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNew;

	public static int DamageBySpellsCount => GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNewCount;

	/// <summary>
	/// Ordered array of spells by damage
	/// </summary>
	public static Il2CppReferenceArray<GameStatsSkillData> DamageBySpellsOrdered =
		SortDamageBySpellsOrdered();
	
	public static Il2CppReferenceArray<GameStatsSkillData> SortDamageBySpellsOrdered()
	{
		Il2CppReferenceArray<GameStatsSkillData> spells = DamageBySpells;
		for (int a = 0; a < spells.Count - 1; a++)
		{
			for (int b = 0; b < spells.Count - a - 1; b++)
			{
				if (spells[b].FloatValue > spells[b + 1].FloatValue)
				{
					(spells[b], spells[b + 1]) = (spells[b + 1], spells[b]);
				}
			}
		}

		return spells;
	}

	/// <summary>
	/// Calculate the total damage done by the player
	/// </summary>
	public static float PlayerTotalDamage
	{
		get
		{
			// create a variable to store the total damage
			float total = 0;
			
			// loop through all spells
			foreach (GameStatsSkillData spellData in GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNew)
			{
				// add the damage to the total
				total += spellData.FloatValue;
			}
			// return the absolute value of the total
			return Mathf.Abs(total);
		}
	}
	
	/// <summary>
	/// Calculate the player DPS (Damage per Second = Total Damage / Fight Duration)
	/// </summary>
	public static float PlayerDps => PlayerTotalDamage / FightDuration;
	
	/// <summary>
	/// Start the Skada history and set the start time
	/// </summary>
	public static void Start()
	{
		// set the start time to the current game time
		SkadaTime.PlayerStartTime = Time.time;
	}
	
	/// <summary>
	/// Stop the Skada history and reset the start time
	/// </summary>
	public static void Reset()
	{
		// reset the start time
		SkadaTime.PlayerStartTime = 0;
	}
}
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
	public static Il2CppReferenceArray<GameStatsSkillData> DamageBySpellsOrdered { get; set; } = Sort();
		
	/// <summary>
	/// Sort the spells by damage in ascending order
	/// </summary>
	/// <returns></returns>
	public static Il2CppReferenceArray<GameStatsSkillData> Sort()
	{
		// get the array of spells
		Il2CppReferenceArray<GameStatsSkillData> spells = DamageBySpells;
		// get the length of the array
		int length = spells.Length;
		// if the array is empty or has only one element
		if (length <= 1)
			// return the array
			return spells;
		
		// sort the array
		Array.Sort<GameStatsSkillData>(spells, (a, b) => b.FloatValue.CompareTo(a.FloatValue));
		
		// return the ordered array
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
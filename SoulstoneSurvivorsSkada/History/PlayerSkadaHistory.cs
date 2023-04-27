using Il2Cpp;
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
	public static bool IsEmpty => GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNewCount == 0;
	
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
	public static Il2CppReferenceArray<GameStatsSkillData> DamageBySpellsOrdered = DamageBySpells;
	
	public static Il2CppReferenceArray<GameStatsSkillData> DpsBySpellsOrdered = DamageBySpells;

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
			for (int index = 0; index < GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNewCount; index++)
			{
				// add the damage to the total
				total += GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNew[index].FloatValue;
			}

			// return the absolute value of the total
			return Mathf.Abs(total);
		}
	}

	// public static void CalculateTotalDamage()
	// {
	// 	// create a variable to store the total damage
	// 	float total = 0;
	// 		
	// 	// loop through all spells
	// 	for (int index = 0; index < GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNewCount; index++)
	// 	{
	// 		// add the damage to the total
	// 		total += GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNew[index].FloatValue;
	// 	}
	//
	// 	// return the absolute value of the total
	// 	_playerTotalDamage = Mathf.Abs(total);
	// }
	
	/// <summary>
	/// Calculate the player DPS (Damage per Second = Total Damage / Fight Duration)
	/// </summary>
	public static float PlayerDps => PlayerTotalDamage / FightDuration;

	public static float CalculatePlayerDps(float time)
	{
		return PlayerTotalDamage / (time - SkadaTime.PlayerStartTime);
	}
	
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
		SkadaTime.ResetAllSpells();
	}
	
	public static void ClearDamageBySpells()
	{
		GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNewCount = 0;
		Il2CppReferenceArray<GameStatsSkillData> skillBuffer = GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNew;
		GameManagerUtil.GameStats.TotalDamageDonePerSkillIdNew = new Il2CppReferenceArray<GameStatsSkillData>(skillBuffer.Count);
	}
	
	public static float GetDPSBySpellId(int spellId)
	{
		if (!SkadaTime.IsSpellActive(spellId)) return 0f;
		foreach (GameStatsSkillData skillData in DamageBySpells)
		{
			if (skillData.SkillNameHash == spellId)
			{
				return skillData.FloatValue / SkadaTime.SpellStartTime[spellId];
			}
		}

		return 0;
	}
}
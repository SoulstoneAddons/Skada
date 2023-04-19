using System.Collections.Generic;
using UnityEngine;

namespace SoulstoneSurvivorsSkada;

public static class SkadaTime
{
	public static float PlayerStartTime { get; set; }
	
	public static Dictionary<int, float> SpellStartTime { get; set; } = new();
	
	public static void StartSpell(int skillId)
	{
		SpellStartTime[skillId] = Time.time;
	}
	
	public static bool IsSpellActive(int skillId)
	{
		return SpellStartTime.ContainsKey(skillId) && SpellStartTime[skillId] > 0;
	}
	
	public static float GetSpellDuration(int skillId)
	{
		if (!SpellStartTime.ContainsKey(skillId)) return 0;
		return Time.time - SpellStartTime[skillId];
	}
	
	public static void ResetSpell(int skillId)
	{
		if (!SpellStartTime.ContainsKey(skillId)) return;
		SpellStartTime[skillId] = 0;
	}
	
	public static void ResetAllSpells()
	{
		SpellStartTime.Clear();
	}
}
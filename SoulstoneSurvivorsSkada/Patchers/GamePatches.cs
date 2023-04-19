using HarmonyLib;
using Il2Cpp;
using SoulstoneSurvivorsSkada.Logging;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Patchers;

/// <summary>
/// Patches for the Game
/// </summary>
internal static class GamePatches
{
	// Hook into the ChangeHealthEffect.DoApplyEffects method to start the SkadaHistory when the first damage is done
	[HarmonyPatch(typeof(ChangeHealthEffect), nameof(ChangeHealthEffect.DoApplyEffects))]
	[HarmonyPostfix]
	public static void DoApplyEffects(ChangeHealthEffect __instance,
		Entity casterEntity,
		StatsComponentReference casterStats,
		PassiveSkillsComponentReference casterPassiveSkills,
		Transform casterReferenceTransform,
		SkillTargetToProcessData target,
		bool useDeltaTime)
	{
		if (!PlayerSkadaHistory.IsStarted)
		{
			PlayerSkadaHistory.Start();
		}
	}
	
	[HarmonyPatch(typeof(GameStats), nameof(GameStats.AddDamageDonePerSkill))]
	[HarmonyPostfix]
	public static void AddDamageDonePerSkill(
		string skillId,
		int skillHash,
		float effectiveDamage,
		float totalDamage,
		string source,
		Object context)
	{
		if (SkadaTime.IsSpellActive(skillHash)) return;
		SkadaTime.StartSpell(skillHash);
	}
}
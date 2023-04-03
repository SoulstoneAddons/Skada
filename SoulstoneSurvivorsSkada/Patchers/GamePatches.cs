using HarmonyLib;
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
	
	// Hook into the ConfirmationPanel.OnConfirmSelected method to reset the SkadaHistory when the player restarts the game
	[HarmonyPatch(typeof(ConfirmationPanel), nameof(ConfirmationPanel.OnConfirmSelected))]
	[HarmonyPostfix]
	public static void OnRestartPressed()
	{
		PlayerSkadaHistory.Reset();
	}
}